using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Xml.Serialization;
using System.Text;

namespace SWQA.Pages.AdminReporting
{
    public class AdminReportingModel : PageModel
    {
        public string ErrorMessage { get; set; } = "";

        public void OnGet()
        {
        }

        public IActionResult OnPostDownloadReport(string ReportType)
        {
            try
            {
                object reportData = null;
                string fileName = $"{ReportType}_{DateTime.Now:yyyyMMdd_HHmm}.xml";
                string conString = "Server=localhost; Database=SWQA; Integrated Security=True; TrustServerCertificate=True;";

                using (SqlConnection connection = new SqlConnection(conString))
                {
                    connection.Open();

                    switch (ReportType)
                    {
                        case "FrequentErrors":
                            reportData = GetFrequentErrors(connection);
                            break;
                        case "LatestBugs":
                            reportData = GetLatestBugs(connection);
                            break;
                        case "ApiBugs":
                            reportData = GetApiBugs(connection);
                            break;
                        case "ActiveEnvs":
                            reportData = GetActiveEnvs(connection);
                            break;
                        case "OsDistribution":
                            reportData = GetOsDistribution(connection);
                            break;
                        case "BugsByDate":
                            reportData = GetBugsByDate(connection);
                            break;
                        default:
                            ErrorMessage = "Invalid report type selected.";
                            return Page();
                    }
                }

                // Translate data into XML
                var serializer = new XmlSerializer(reportData.GetType());
                using (var memoryStream = new MemoryStream())
                {
                    using (var streamWriter = new StreamWriter(memoryStream, Encoding.UTF8))
                    {
                        serializer.Serialize(streamWriter, reportData);
                    }
                    return File(memoryStream.ToArray(), "application/xml", fileName);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "An error occurred while generating the report: " + ex.Message;
                return Page();
            }
        }

        // 1. Most frequently used modules
        private List<FrequentErrorRecord> GetFrequentErrors(SqlConnection connection)
        {
            var list = new List<FrequentErrorRecord>();
            string sql = "SELECT ErrorCode, COUNT(*) as Count FROM BugReport GROUP BY ErrorCode ORDER BY Count DESC";
            using (SqlCommand cmd = new SqlCommand(sql, connection))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read()) list.Add(new FrequentErrorRecord { ErrorCode = reader.GetString(0), Count = reader.GetInt32(1) });
            }
            return list;
        }

        // 2. Last 10 bugs
        private List<SWQA.Pages.BugReport.BugReport> GetLatestBugs(SqlConnection connection)
        {
            var list = new List<SWQA.Pages.BugReport.BugReport>();
            string sql = "SELECT TOP 10 BugId, ErrorCode, BugDescription, DateReported FROM BugReport ORDER BY DateReported DESC";
            using (SqlCommand cmd = new SqlCommand(sql, connection))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read()) list.Add(new SWQA.Pages.BugReport.BugReport { BugId = reader.GetInt32(0), ErrorCode = reader.GetString(1), BugDescription = reader.GetString(2), DateReported = reader.GetDateTime(3) });
            }
            return list;
        }

        // 3. API errors
        private List<SWQA.Pages.BugReport.BugReport> GetApiBugs(SqlConnection connection)
        {
            var list = new List<SWQA.Pages.BugReport.BugReport>();
            string sql = "SELECT BugId, ErrorCode, BugDescription, DateReported FROM BugReport WHERE ErrorCode LIKE 'API%'";
            using (SqlCommand cmd = new SqlCommand(sql, connection))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read()) list.Add(new SWQA.Pages.BugReport.BugReport { BugId = reader.GetInt32(0), ErrorCode = reader.GetString(1), BugDescription = reader.GetString(2), DateReported = reader.GetDateTime(3) });
            }
            return list;
        }

        // 4. Active test environments
        private List<SWQA.Pages.TestEnvironment.TestEnvironment> GetActiveEnvs(SqlConnection connection)
        {
            var list = new List<SWQA.Pages.TestEnvironment.TestEnvironment>();
            string sql = "SELECT TableId, ServerIp, OperatingSystem, IsActive FROM TestEnvironment WHERE IsActive = 1";
            using (SqlCommand cmd = new SqlCommand(sql, connection))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read()) list.Add(new SWQA.Pages.TestEnvironment.TestEnvironment { TableId = reader.GetInt32(0), ServerIp = reader.GetString(1), OperatingSystem = reader.GetString(2), IsActive = reader.GetBoolean(3) });
            }
            return list;
        }

        // 5. OS profile
        private List<OsDistributionRecord> GetOsDistribution(SqlConnection connection)
        {
            var list = new List<OsDistributionRecord>();
            string sql = "SELECT OperatingSystem, COUNT(*) as Count FROM TestEnvironment GROUP BY OperatingSystem ORDER BY Count DESC";
            using (SqlCommand cmd = new SqlCommand(sql, connection))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read()) list.Add(new OsDistributionRecord { OperatingSystem = reader.GetString(0), Count = reader.GetInt32(1) });
            }
            return list;
        }

        // 6. Bug logged date Z-A
        private List<BugsByDateRecord> GetBugsByDate(SqlConnection connection)
        {
            var list = new List<BugsByDateRecord>();
            string sql = "SELECT CAST(DateReported AS DATE) as ReportDate, COUNT(*) as Count FROM BugReport GROUP BY CAST(DateReported AS DATE) ORDER BY ReportDate DESC";
            using (SqlCommand cmd = new SqlCommand(sql, connection))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read()) list.Add(new BugsByDateRecord { ReportDate = reader.GetDateTime(0).ToString("yyyy-MM-dd"), Count = reader.GetInt32(1) });
            }
            return list;
        }
    }

    // --- XML serialization constructors ---
    public class FrequentErrorRecord { public string ErrorCode { get; set; } public int Count { get; set; } }
    public class OsDistributionRecord { public string OperatingSystem { get; set; } public int Count { get; set; } }
    public class BugsByDateRecord { public string ReportDate { get; set; } public int Count { get; set; } }
}
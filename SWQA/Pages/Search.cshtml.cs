using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Text.RegularExpressions;

namespace SWQA.Pages
{
    public class SearchModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string q { get; set; }

        public List<SWQA.Pages.BugReport.BugReport> BugResults { get; set; } = new List<SWQA.Pages.BugReport.BugReport>();

        public List<SWQA.Pages.TestEnvironment.TestEnvironment> EnvResults { get; set; } = new List<SWQA.Pages.TestEnvironment.TestEnvironment>();

        public HtmlString HighlightText(string text, string query)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(query))
            {
                return new HtmlString(System.Net.WebUtility.HtmlEncode(text ?? ""));
            }

            string encodedText = System.Net.WebUtility.HtmlEncode(text);
            string encodedQuery = System.Net.WebUtility.HtmlEncode(query);

            string pattern = Regex.Escape(encodedQuery);
            string highlighted = Regex.Replace(encodedText, pattern, "<mark class='bg-warning'>$0</mark>", RegexOptions.IgnoreCase);

            return new HtmlString(highlighted);
        }

        public void OnGet()
        {
            if (string.IsNullOrWhiteSpace(q)) return;

            string conString = "Server=localhost; Database = SWQA; Integrated Security = True; TrustServerCertificate = True;";

            using (SqlConnection connection = new SqlConnection(conString))
            {
                connection.Open();

                string bugSql = "SELECT * FROM BugReport WHERE ErrorCode LIKE @search OR BugDescription LIKE @search";
                using (SqlCommand cmd = new SqlCommand(bugSql, connection))
                {
                    cmd.Parameters.AddWithValue("@search", "%" + q + "%");
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BugResults.Add(new SWQA.Pages.BugReport.BugReport
                            {
                                BugId = reader.GetInt32(0),
                                ErrorCode = reader.GetString(1),
                                BugDescription = reader.GetString(2),
                                DateReported = reader.GetDateTime(3)
                            });
                        }
                    }
                }

                string envSql = "SELECT * FROM TestEnvironment WHERE ServerIp LIKE @search OR OperatingSystem LIKE @search";
                using (SqlCommand cmd = new SqlCommand(envSql, connection))
                {
                    cmd.Parameters.AddWithValue("@search", "%" + q + "%");
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            EnvResults.Add(new SWQA.Pages.TestEnvironment.TestEnvironment
                            {
                                TableId = reader.GetInt32(0),
                                ServerIp = reader.GetString(1),
                                OperatingSystem = reader.GetString(2),
                                IsActive = reader.GetBoolean(3)
                            });
                        }
                    }
                }
            }
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace SWQA.Pages.BugReport
{
    public class IndexModel : PageModel
    {
        [BindProperty]

        public List<BugReport> Listele { get; set; } = new List<BugReport>();

        public void OnGet()
        {
            string conString = "Server=localhost; Database = SWQA; Integrated Security = True; TrustServerCertificate = True;";

            try
            {
                using (SqlConnection connection = new SqlConnection(conString))
                {
                    connection.Open();

                    string sql = "SELECT * FROM BugReport";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                BugReport bugreport = new BugReport
                                {
                                    BugId = reader.GetInt32(0),
                                    ErrorCode = reader.IsDBNull(1) ? "" : reader.GetString(1),
                                    BugDescription = reader.IsDBNull(2) ? "" : reader.GetString(2),
                                    DateReported = reader.IsDBNull(3) ? DateTime.MinValue : reader.GetDateTime(3)
                                };

                                Listele.Add(bugreport);
                            }
                        }
                    }
                }
            }
            catch
            {

            }
        }
    }
}

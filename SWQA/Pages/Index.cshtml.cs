using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace SWQA.Pages
{
    public class IndexModel : PageModel
    {
        public int TotalActiveEnvironments { get; set; }
        public int TotalBugsLogged { get; set; }

        public void OnGet()
        {
            string conString = "Server=localhost; Database = SWQA; Integrated Security = True; TrustServerCertificate = True;";

            using (SqlConnection connection = new SqlConnection(conString))
            {
                connection.Open();

                // Get Active Environments Count
                string envirQuery = "SELECT COUNT(*) FROM TestEnvironment WHERE IsActive = 1";

                using (SqlCommand cmd = new SqlCommand(envirQuery, connection))
                {
                    TotalActiveEnvironments = (int)cmd.ExecuteScalar();
                }

                // Get Total Bugs Count
                string bugQuery = "SELECT COUNT(*) FROM BugReport";

                using (SqlCommand cmd = new SqlCommand(bugQuery, connection))
                {
                    TotalBugsLogged = (int)cmd.ExecuteScalar();
                }
            }
        }
    }
}

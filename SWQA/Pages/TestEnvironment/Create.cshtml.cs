using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using SWQA.Pages.BugReport;

namespace SWQA.Pages.TestEnvironment
{
    public class CreateModel : PageModel
    {
        public TestEnvironment testenvironment = new TestEnvironment();

        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
            
        }

        public void OnPost()
        {
            testenvironment.ServerIp = Request.Form["ServerIp"].ToString();
            testenvironment.OperatingSystem = Request.Form["OperatingSystem"].ToString();
            string rawStatus = Request.Form["IsActive"].ToString();

            if (testenvironment.ServerIp.Length == 0 || testenvironment.OperatingSystem.Length == 0 || rawStatus.Length == 0)
            {
                errorMessage = "All fields are required!";
                return;
            }

            testenvironment.IsActive = bool.Parse(rawStatus);

            try
            {
                string conString = "Server=localhost; Database = SWQA; Integrated Security = True; TrustServerCertificate = True;";

                using (SqlConnection connection = new SqlConnection(conString))
                {
                    connection.Open();

                    string sql = "INSERT INTO testenvironment(ServerIp, OperatingSystem, IsActive) VALUES(@ServerIp, @OperatingSystem, @IsActive)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ServerIp", testenvironment.ServerIp);
                        command.Parameters.AddWithValue("@OperatingSystem", testenvironment.OperatingSystem);
                        command.Parameters.AddWithValue("@IsActive", testenvironment.IsActive);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            successMessage = "Saved successfully!";
            Response.Redirect("/TestEnvironment/Index");
        }
    }
}

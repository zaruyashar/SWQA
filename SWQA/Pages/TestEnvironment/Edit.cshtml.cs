using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace SWQA.Pages.TestEnvironment
{
    public class EditModel : PageModel
    {
        public TestEnvironment testenvironment = new TestEnvironment();

        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
            string conString = "Server=localhost; Database = SWQA; Integrated Security = True; TrustServerCertificate = True;";

            string ID = Request.Query["ID"];

            try
            {
                using (SqlConnection connection = new SqlConnection(conString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM TestEnvironment WHERE TableId = @TableId";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@TableId", ID);
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            testenvironment.TableId = reader.GetInt32(0);
                            testenvironment.ServerIp = reader.GetString(1);
                            testenvironment.OperatingSystem = reader.GetString(2);
                            testenvironment.IsActive = reader.GetBoolean(3);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            testenvironment.TableId = int.Parse(Request.Form["TableId"].ToString());
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
                    string Sql = "UPDATE testenvironment SET ServerIp = @ServerIp, OperatingSystem = @OperatingSystem, IsActive = @IsActive WHERE TableId = @TableId";

                    using (SqlCommand command = new SqlCommand(Sql, connection))
                    {
                        command.Parameters.AddWithValue("@TableId", testenvironment.TableId);
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

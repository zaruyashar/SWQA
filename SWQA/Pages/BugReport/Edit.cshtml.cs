using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace SWQA.Pages.BugReport
{
    public class EditModel : PageModel
    {
        public BugReport bugreport = new BugReport();
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
                    string sql = "SELECT * FROM BugReport WHERE BugId = @BugId";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@BugId", ID);
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            bugreport.BugId = reader.GetInt32(0);
                            bugreport.ErrorCode = reader.GetString(1);
                            bugreport.BugDescription = reader.GetString(2);
                            bugreport.DateReported = reader.GetDateTime(3);
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
            bugreport.BugId = int.Parse(Request.Form["BugId"].ToString());
            bugreport.ErrorCode = Request.Form["ErrorCode"].ToString();
            bugreport.BugDescription = Request.Form["BugDescription"].ToString();
            string rawDate = Request.Form["DateReported"].ToString();

            if (bugreport.ErrorCode.Length == 0 ||
                bugreport.BugDescription.Length == 0 ||
                rawDate.Length == 0)
            {
                errorMessage = "All fields are required!";
                return;
            }

            bugreport.DateReported = DateTime.Parse(rawDate);

            try
            {
                string conString = "Server=localhost; Database = SWQA; Integrated Security = True; TrustServerCertificate = True;";

                using (SqlConnection connection = new SqlConnection(conString))
                {
                    connection.Open();
                    string Sql = "UPDATE BugReport SET ErrorCode = @ErrorCode, BugDescription = @BugDescription, DateReported = @DateReported WHERE BugId = @BugId";

                    using (SqlCommand command = new SqlCommand(Sql, connection))
                    {
                        command.Parameters.AddWithValue("@ErrorCode", bugreport.ErrorCode);
                        command.Parameters.AddWithValue("@BugDescription", bugreport.BugDescription);
                        command.Parameters.AddWithValue("@DateReported", bugreport.DateReported);
                        command.Parameters.AddWithValue("@BugId", bugreport.BugId);

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
            Response.Redirect("/BugReport/Index");
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace SWQA.Pages.BugReport
{
    public class CreateModel : PageModel
    {
        public BugReport bugreport = new BugReport();

        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
            bugreport.DateReported = DateTime.Now;
        }

        public void OnPost()
        {
            bugreport.ErrorCode = Request.Form["ErrorCode"].ToString();
            bugreport.BugDescription = Request.Form["BugDescription"].ToString();
            string rawDate = Request.Form["DateReported"].ToString();

            if (bugreport.ErrorCode.Length == 0 || bugreport.BugDescription.Length == 0 || rawDate.Length == 0)
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

                    string sql = "INSERT INTO BugReport(ErrorCode, BugDescription, DateReported) VALUES(@ErrorCode, @BugDescription, @DateReported)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ErrorCode", bugreport.ErrorCode);
                        command.Parameters.AddWithValue("@BugDescription", bugreport.BugDescription);
                        command.Parameters.AddWithValue("@DateReported", bugreport.DateReported);

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

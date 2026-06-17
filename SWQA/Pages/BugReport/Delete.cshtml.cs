using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace SWQA.Pages.BugReport
{
    public class DeleteModel : PageModel
    {
        public BugReport bugreport = new BugReport();

        public IActionResult OnPost(int id)
        {
            string conString = "Server=localhost; Database = SWQA; Integrated Security = True; TrustServerCertificate = True;";

            string ID = Request.Query["ID"];

            using (SqlConnection connection = new SqlConnection(conString))
            {

                connection.Open();
                string sql = "DELETE FROM BugReport WHERE BugId = @BugId";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@BugId", ID);
                    command.ExecuteNonQuery();
                }
            }

            return RedirectToPage("/BugReport/Index");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace SWQA.Pages.TestEnvironment
{
    public class DeleteModel : PageModel
    {
        public TestEnvironment testenvironment = new TestEnvironment();

        public IActionResult OnPost(int id)
        {
            string conString = "Server=localhost; Database = SWQA; Integrated Security = True; TrustServerCertificate = True;";

            string ID = Request.Query["ID"];

            using (SqlConnection connection = new SqlConnection(conString))
            {

                connection.Open();
                string sql = "DELETE FROM TestEnvironment WHERE TableId = @TableId";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@TableId", ID);
                    command.ExecuteNonQuery();
                }
            }

            return RedirectToPage("/TestEnvironment/Index");
        }
    }
}

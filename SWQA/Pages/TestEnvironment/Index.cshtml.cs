using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace SWQA.Pages.TestEnvironment
{
    public class IndexModel : PageModel
    {
        [BindProperty]

        public List<TestEnvironment> Listele { get; set; } = new List<TestEnvironment>();

        public void OnGet()
        {
            string conString = "Server=localhost; Database = SWQA; Integrated Security = True; TrustServerCertificate = True;";

            try
            {
                using (SqlConnection connection = new SqlConnection(conString))
                {
                    connection.Open();

                    string sql = "SELECT * FROM TestEnvironment";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TestEnvironment testenvironment = new TestEnvironment
                                {
                                    TableId = reader.GetInt32(0),
                                    ServerIp = reader.GetString(1),
                                    OperatingSystem = reader.GetString(2),
                                    IsActive = reader.GetBoolean(3)
                                };

                                Listele.Add(testenvironment);
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

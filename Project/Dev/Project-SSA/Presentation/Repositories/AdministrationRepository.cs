using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Repositories
{
    public class AdministrationRepository
    {
        public static void PurgeUsers()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "UPDATE dbo.AspNetUsers SET LockoutEndDateUtc = '2999-12-31' WHERE LastLogin < CURRENT_TIMESTAMP - 60;";

                    cmd.ExecuteNonQuery();
                }

                con.Close();
            }
        }
    }
}

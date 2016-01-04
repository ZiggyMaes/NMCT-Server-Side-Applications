using Presentation.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Repositories
{
    public class UserRepository
    {
        public static User GetUser(string UserId)
        {
            User FoundUser = new User();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT u.Id, u.Email, u.UserName, ur.RoleId, r.Name FROM dbo.AspNetUsers as u INNER JOIN dbo.AspNetUserRoles as ur ON u.Id = ur.UserId INNER JOIN dbo.AspNetRoles as r ON ur.RoleId = r.Id ORDER BY u.Email";

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader == null) return null;
                    while (reader.Read())
                    {
                        FoundUser.UserId = (reader["Id"] == DBNull.Value ? string.Empty : reader["Id"].ToString());
                        FoundUser.UserName = (reader["UserName"] == DBNull.Value ? string.Empty : reader["UserName"].ToString());
                        FoundUser.Email = (reader["Email"] == DBNull.Value ? string.Empty : reader["Email"].ToString());
                        FoundUser.Role = (reader["Name"] == DBNull.Value ? string.Empty : reader["Name"].ToString());
                        //FoundUser.Area1 = (reader["Area1"] == DBNull.Value ? string.Empty : reader["Area1"].ToString());
                        //FoundUser.Area2 = (reader["Area2"] == DBNull.Value ? string.Empty : reader["Area2"].ToString());
                    }
                }

                con.Close();
            }
            return FoundUser;
        }
    }
}

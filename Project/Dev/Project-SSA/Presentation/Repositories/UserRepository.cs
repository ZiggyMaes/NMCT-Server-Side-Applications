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
                    cmd.CommandText = "SELECT u.Id, u.DisplayName, u.Area1, u.Area2, u.Email, r.Name FROM dbo.AspNetUsers as u INNER JOIN dbo.AspNetUserRoles as ur ON u.Id = ur.UserId INNER JOIN dbo.AspNetRoles as r ON ur.RoleId = r.Id WHERE u.Id = @UserId";
                    cmd.Parameters.AddWithValue("UserId", UserId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader == null) return null;
                    while (reader.Read())
                    {
                        FoundUser.UserId = (reader["Id"] == DBNull.Value ? string.Empty : reader["Id"].ToString());
                        FoundUser.DisplayName = (reader["DisplayName"] == DBNull.Value ? string.Empty : reader["DisplayName"].ToString());
                        FoundUser.Email = (reader["Email"] == DBNull.Value ? string.Empty : reader["Email"].ToString());
                        FoundUser.Role = (reader["Name"] == DBNull.Value ? string.Empty : reader["Name"].ToString());
                        FoundUser.Area1 = Convert.ToInt32((reader["Area1"] == DBNull.Value ? string.Empty : reader["Area1"]));
                        FoundUser.Area2 = Convert.ToInt32((reader["Area2"] == DBNull.Value ? string.Empty : reader["Area2"]));
                    }
                }
                con.Close();
            }
            return FoundUser;
        }
        public static void LockUser(string UserId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "UPDATE dbo.AspNetUsers SET LockoutEndDateUtc = '2999-12-31' WHERE UserId = @UserId;";
                    cmd.Parameters.AddWithValue("UserId", UserId);

                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }
        public static void SetRole(string UserId, int RoleValue)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "UPDATE dbo.AspNetUserRoles SET RoleId = @RoleValue WHERE UserId = @UserId;";
                    cmd.Parameters.AddWithValue("RoleValue", RoleValue);
                    cmd.Parameters.AddWithValue("UserId", UserId);

                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }
    }
}

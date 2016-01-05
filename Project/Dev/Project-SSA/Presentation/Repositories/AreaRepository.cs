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
    public class AreaRepository
    {
        private string CONNECTIONSTRING = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static Area GetAreaInfo(int AreaId)
        {
            Area area = new Area();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT Id, Title, Description FROM dbo.Area WHERE Id = @AreaId";
                    cmd.Parameters.AddWithValue("AreaId", AreaId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (!reader.HasRows) return null;
                    while (reader.Read())
                    {
                        area.Id = int.Parse(reader["Id"].ToString());
                        area.Title = (reader["Title"] == DBNull.Value ? string.Empty : reader["Title"].ToString());
                        area.Description = (reader["Description"] == DBNull.Value ? string.Empty : reader["Description"].ToString());
                    } 
                }
                con.Close();
            }
            return area;
        }
        public static List<Area> GetAreas()
        {
            List<Area> Areas = new List<Area>();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT Id, Title, Description FROM dbo.Area";

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (!reader.HasRows) return null;
                    while (reader.Read())
                    {
                        Area a = new Area();

                        a.Id = int.Parse(reader["Id"].ToString());
                        a.Title = (reader["Title"] == DBNull.Value ? string.Empty : reader["Title"].ToString());
                        a.Description = (reader["Description"] == DBNull.Value ? string.Empty : reader["Description"].ToString());

                        Areas.Add(a);
                    }
                }
                con.Close();
            }
            return Areas;
        }
        public static void UpdateUserAreas(string UserId, int[] Areas)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "UPDATE dbo.AspNetUsers SET Area1 = @Area1, Area2 = @Area2 WHERE Id = @UserId;";
                    cmd.Parameters.AddWithValue("Area1", Areas[0]);
                    cmd.Parameters.AddWithValue("Area2", Areas[1]);
                    cmd.Parameters.AddWithValue("UserId", UserId);

                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }
    }
}

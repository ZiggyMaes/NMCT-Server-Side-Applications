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
    }
}

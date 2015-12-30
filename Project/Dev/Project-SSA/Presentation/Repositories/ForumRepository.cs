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
    public class ForumRepository
    {
        private string CONNECTIONSTRING = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static Message GetMessage(int PostId)
        {
            Message message = new Message();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT Id, Title, Body, TimePosted, ParentId, Visible, AreaId, UserId FROM dbo.Message WHERE Id = @PostId";
                    cmd.Parameters.AddWithValue("PostId", PostId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (!reader.HasRows) return null;
                    while (reader.Read())
                    {
                        message.Id = int.Parse(reader["Id"].ToString());
                        message.Title = (reader["Title"] == DBNull.Value ? string.Empty : reader["Title"].ToString());
                        message.Body = (reader["Body"] == DBNull.Value ? string.Empty : reader["Description"].ToString());
                        message.TimePosted = (reader["TimePosted"] == DBNull.Value ? DateTime.Now : DateTime.ParseExact(reader["TimePosted"].ToString(), "ddMMYYYY HH:mm", null));
                        message.ParentId = int.Parse(reader["ParentId"].ToString());
                        message.Visible = bool.Parse(reader["Visible"].ToString());
                        message.AreaId = int.Parse(reader["AreaId"].ToString());
                        message.UserId = int.Parse(reader["UserId"].ToString());
                    }
                }
            }
            return message;
        }
    }
}

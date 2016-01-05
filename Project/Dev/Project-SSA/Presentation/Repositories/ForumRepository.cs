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

        public static Message GetMessage(int MessageId)
        {
            Message message = new Message();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT Id, Title, Body, TimePosted, ParentId, Visible, AreaId, UserId FROM dbo.Message WHERE Id = @MessageId";
                    cmd.Parameters.AddWithValue("MessageId", MessageId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (!reader.HasRows) return null;
                    while (reader.Read())
                    {
                        message.Id = int.Parse(reader["Id"].ToString());
                        message.Title = (reader["Title"] == DBNull.Value ? string.Empty : reader["Title"].ToString());
                        message.Body = (reader["Body"] == DBNull.Value ? string.Empty : reader["Body"].ToString());
                        message.TimePosted = (reader["TimePosted"] == DBNull.Value ? DateTime.Now : DateTime.Parse(reader["TimePosted"].ToString()));
                        message.ParentId = int.Parse(reader["ParentId"].ToString());
                        message.Visible = bool.Parse(reader["Visible"].ToString());
                        message.AreaId = int.Parse(reader["AreaId"].ToString());
                        message.UserInfo = UserRepository.GetUser((reader["UserId"] == DBNull.Value ? string.Empty : reader["UserId"].ToString()));
                    }
                }
                con.Close();
            }
            return message;
        }
        public static List<Message> GetMessages(int ThreadId)
        {
            List<Message> Messages = new List<Message>();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT Id, Title, Body, TimePosted, ParentId, Visible, AreaId, UserId FROM dbo.Message WHERE ParentId = @ThreadId";
                    cmd.Parameters.AddWithValue("ThreadId", ThreadId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (!reader.HasRows) return null;
                    while (reader.Read())
                    {
                        Message m = new Message();

                        m.Id = int.Parse(reader["Id"].ToString());
                        m.Title = (reader["Title"] == DBNull.Value ? string.Empty : reader["Title"].ToString());
                        m.Body = (reader["Body"] == DBNull.Value ? string.Empty : reader["Body"].ToString());
                        m.TimePosted = (reader["TimePosted"] == DBNull.Value ? DateTime.Now : DateTime.Parse(reader["TimePosted"].ToString()));
                        m.ParentId = int.Parse(reader["ParentId"].ToString());
                        m.Visible = bool.Parse(reader["Visible"].ToString());
                        m.AreaId = int.Parse(reader["AreaId"].ToString());
                        m.UserInfo = UserRepository.GetUser((reader["UserId"] == DBNull.Value ? string.Empty : reader["UserId"].ToString()));

                        Messages.Add(m);
                    }
                }
                con.Close();
            }
            return Messages;
        }

        public static List<Message> GetThreads(int AreaId)
        {
            List<Message> Threads = new List<Message>();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT Id, Title, Body, TimePosted, ParentId, Visible, AreaId, UserId FROM dbo.Message WHERE AreaId = @AreaId AND Id = ParentId";

                    cmd.Parameters.AddWithValue("AreaId", AreaId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (!reader.HasRows) return null;
                    while (reader.Read())
                    {
                        Message m = new Message();

                        m.Id = int.Parse(reader["Id"].ToString());
                        m.Title = (reader["Title"] == DBNull.Value ? string.Empty : reader["Title"].ToString());
                        m.Body = (reader["Body"] == DBNull.Value ? string.Empty : reader["Body"].ToString());
                        m.TimePosted = (reader["TimePosted"] == DBNull.Value ? DateTime.Now : DateTime.Parse(reader["TimePosted"].ToString()));
                        m.ParentId = int.Parse(reader["ParentId"].ToString());
                        m.Visible = bool.Parse(reader["Visible"].ToString());
                        m.AreaId = int.Parse(reader["AreaId"].ToString());
                        m.UserInfo = UserRepository.GetUser((reader["UserId"] == DBNull.Value ? string.Empty : reader["UserId"].ToString()));
                        m.PostCount = GetPostcount(int.Parse(reader["Id"].ToString()));

                        Threads.Add(m);
                    }
                }
                con.Close();
            }

            return Threads;
        }
        public static int GetPostcount(int ThreadId)
        {
            int posts = 0;

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT COUNT(ParentId) FROM dbo.Message WHERE ParentId = @ThreadId";
                    cmd.Parameters.AddWithValue("ThreadId", ThreadId);

                    posts = Convert.ToInt32(cmd.ExecuteScalar()) - 1;//Thread headline does not count as post (even though it is a post)
                }
                con.Close();
            }
            return posts;
        }
        public static int GetPostcount(string UserId)
        {
            int posts = 0;

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT COUNT(ParentId) FROM dbo.Message WHERE UserId = @UserId";
                    cmd.Parameters.AddWithValue("UserId", UserId);

                    posts = Convert.ToInt32(cmd.ExecuteScalar());
                }
                con.Close();
            }
            return posts;
        }
        public static int AddMessage(Message message)
        {
            int MessageId = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "INSERT INTO dbo.Message (Title, Body, TimePosted, ParentId, Visible, AreaId, UserId) VALUES(@Title, @Body, @TimePosted, @ParentId, @Visible, @AreaId, @UserId); SELECT @@IDENTITY";
                    cmd.Parameters.AddWithValue("Title", message.Title);
                    cmd.Parameters.AddWithValue("Body", message.Body);
                    cmd.Parameters.AddWithValue("TimePosted", DateTime.Now);
                    cmd.Parameters.AddWithValue("ParentId", message.ParentId); //-1 for threads, other value for message
                    cmd.Parameters.AddWithValue("Visible", true);
                    cmd.Parameters.AddWithValue("AreaId", message.AreaId);
                    cmd.Parameters.AddWithValue("UserId", message.UserInfo.UserId);

                    MessageId = int.Parse(cmd.ExecuteScalar().ToString());
                }

                con.Close();
                return MessageId;
            }
        }
        public static void UpdateParentId(int MessageId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "UPDATE dbo.Message SET ParentId = Id WHERE Id = @Id;";
                    cmd.Parameters.AddWithValue("Id", MessageId);

                    cmd.ExecuteNonQuery();
                }

                con.Close();
            }
        }
        public static void HideMessage(int MessageId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "UPDATE dbo.Message SET Visible = '0' WHERE Id = @MessageId";
                    cmd.Parameters.AddWithValue("MessageId", MessageId);

                    cmd.ExecuteNonQuery();
                }

                con.Close();
            }
        }
        public static void AddRating(Rating rating)
        {
            int MessageId = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "INSERT INTO dbo.Message (Stars, MessageId, UserId) VALUES(@Stars, @MessageId, @UserId);";
                    cmd.Parameters.AddWithValue("Stars", rating.Stars);
                    cmd.Parameters.AddWithValue("MessageId", rating.MessageId);
                    cmd.Parameters.AddWithValue("UserId", rating.UserId);

                    MessageId = int.Parse(cmd.ExecuteScalar().ToString());
                }

                con.Close();
            }
        }
        public static List<Rating> GetRatings(int MessageId)
        {
            List<Rating> Ratings = new List<Rating>();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT Id, Stars, MessageId, UserId FROM dbo.Message WHERE MessageId = @MessageId";

                    cmd.Parameters.AddWithValue("MessageId", MessageId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (!reader.HasRows) return null;
                    while (reader.Read())
                    {
                        Rating r = new Rating();

                        r.Id = int.Parse(reader["Id"].ToString());
                        r.Stars = int.Parse(reader["Stars"].ToString());
                        r.MessageId = int.Parse(reader["MessageId"].ToString());
                        r.UserId = (reader["UserId"] == DBNull.Value ? string.Empty : reader["UserId"].ToString());
                        r.UserName = UserRepository.GetUser(r.UserId).DisplayName;

                        Ratings.Add(r);
                    }
                }
                con.Close();
            }
            return Ratings;
        }
        public static List<Message> Search(int AreaId, string Query)
        {
            List<Message> Threads = new List<Message>();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT Id, Title, Body, TimePosted, ParentId, Visible, AreaId, UserId FROM dbo.Message WHERE AreaId = @AreaId AND Body LIKE @Query";

                    cmd.Parameters.AddWithValue("AreaId", AreaId);
                    cmd.Parameters.AddWithValue("Query", "%" + Query + "%");

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (!reader.HasRows) return null;
                    while (reader.Read())
                    {
                        Message m = new Message();

                        m.Id = int.Parse(reader["Id"].ToString());
                        m.Title = (reader["Title"] == DBNull.Value ? string.Empty : reader["Title"].ToString());
                        m.Body = (reader["Body"] == DBNull.Value ? string.Empty : reader["Body"].ToString());
                        m.TimePosted = (reader["TimePosted"] == DBNull.Value ? DateTime.Now : DateTime.Parse(reader["TimePosted"].ToString()));
                        m.ParentId = int.Parse(reader["ParentId"].ToString());
                        m.Visible = bool.Parse(reader["Visible"].ToString());
                        m.AreaId = int.Parse(reader["AreaId"].ToString());
                        m.UserInfo = UserRepository.GetUser((reader["UserId"] == DBNull.Value ? string.Empty : reader["UserId"].ToString()));
                        m.PostCount = GetPostcount(int.Parse(reader["Id"].ToString()));

                        Threads.Add(m);
                    }
                }
                con.Close();
            }

            return Threads;
        }
    }
}

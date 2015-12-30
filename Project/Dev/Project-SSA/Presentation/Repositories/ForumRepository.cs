﻿using Presentation.Models;
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
                        message.Body = (reader["Body"] == DBNull.Value ? string.Empty : reader["Description"].ToString());
                        message.TimePosted = (reader["TimePosted"] == DBNull.Value ? DateTime.Now : DateTime.ParseExact(reader["TimePosted"].ToString(), "ddMMYYYY HH:mm", null));
                        message.ParentId = int.Parse(reader["ParentId"].ToString());
                        message.Visible = bool.Parse(reader["Visible"].ToString());
                        message.AreaId = int.Parse(reader["AreaId"].ToString());
                        message.UserId = int.Parse(reader["UserId"].ToString());
                    }
                }
                con.Close();
            }
            return message;
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
                    cmd.CommandText = "SELECT Id, Title, Body, TimePosted, ParentId, Visible, AreaId, UserId FROM dbo.Message WHERE ParentId = @ParentId";

                    cmd.Parameters.AddWithValue("ParentId", -1);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Message m = new Message();

                        m.Id = int.Parse(reader["Id"].ToString());
                        m.Title = (reader["Title"] == DBNull.Value ? string.Empty : reader["Title"].ToString());
                        m.Body = (reader["Body"] == DBNull.Value ? string.Empty : reader["Description"].ToString());
                        m.TimePosted = (reader["TimePosted"] == DBNull.Value ? DateTime.Now : DateTime.ParseExact(reader["TimePosted"].ToString(), "ddMMYYYY HH:mm", null));
                        m.ParentId = int.Parse(reader["ParentId"].ToString());
                        m.Visible = bool.Parse(reader["Visible"].ToString());
                        m.AreaId = int.Parse(reader["AreaId"].ToString());
                        m.UserId = int.Parse(reader["UserId"].ToString());

                        Threads.Add(m);
                    }
                }
                con.Close();
            }

            return Threads;
        }
        private static int GetPostcount(int ThreadId)
        {
            int posts = 0;

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT COUNT(Id) FROM dbo.Message WHERE Id = @ThreadId";
                    cmd.Parameters.AddWithValue("threadId", ThreadId);

                    posts = Convert.ToInt32(cmd.ExecuteScalar());

                }

                con.Close();
            }

            return posts;
        }
        public static int Addmessage(bool IsThread, Message message)
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
                    if(IsThread) cmd.Parameters.AddWithValue("ParentId", -1); //ParentId is -1 for threads/topics
                    else cmd.Parameters.AddWithValue("ParentId", message.ParentId); //Parent MessageId for regular posts
                    cmd.Parameters.AddWithValue("Visible", true);
                    cmd.Parameters.AddWithValue("AreaId", message.AreaId);
                    cmd.Parameters.AddWithValue("UserId", message.UserId);

                    MessageId = int.Parse(cmd.ExecuteScalar().ToString());
                }

                con.Close();
                return MessageId;
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
    }
}
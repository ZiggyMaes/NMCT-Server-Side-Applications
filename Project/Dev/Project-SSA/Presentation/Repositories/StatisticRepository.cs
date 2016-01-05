using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Repositories
{
    public class StatisticRepository
    {
        public static int GetNewThreadCount()
        {
            int posts = 0;

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT COUNT(Id) FROM dbo.Message WHERE Id = ParentId AND TimePosted > CURRENT_TIMESTAMP - 1";

                    posts = Convert.ToInt32(cmd.ExecuteScalar());
                }
                con.Close();
            }
            return posts;
        }
        public static int GetNewPostCount()
        {
            int posts = 0;

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT COUNT(Id) FROM dbo.Message WHERE Id <> ParentId AND TimePosted > CURRENT_TIMESTAMP - 1";

                    posts = Convert.ToInt32(cmd.ExecuteScalar());
                }
                con.Close();
            }
            return posts;
        }
        public static int GetTotalThreadCount()
        {
            int posts = 0;

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT COUNT(Id) FROM dbo.Message WHERE Id = ParentId";

                    posts = Convert.ToInt32(cmd.ExecuteScalar());
                }
                con.Close();
            }
            return posts;
        }
        public static int GetTotalPostCount()
        {
            int posts = 0;

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT COUNT(Id) FROM dbo.Message WHERE Id <> ParentId";

                    posts = Convert.ToInt32(cmd.ExecuteScalar());
                }
                con.Close();
            }
            return posts;
        }
    }
}

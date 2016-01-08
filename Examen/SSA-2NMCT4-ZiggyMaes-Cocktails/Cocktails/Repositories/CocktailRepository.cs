using Cocktails.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Cocktails.Repositories
{
    public class CocktailRepository
    {

        private string ConnectionString { get; set; }

        public CocktailRepository()
        {
            this.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }


        public Cocktail GetCocktailByID(int cocktailID)
        {
            Cocktail cocktail = null;

            using (SqlConnection con = new SqlConnection(this.ConnectionString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand())
                {

                    string sSQL = "SELECT * FROM Cocktails ";
                    sSQL += " WHERE ID = @cocktailID";

                    command.CommandText = sSQL;
                    command.Parameters.AddWithValue("@cocktailID", cocktailID);
                    command.Connection = con;

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        cocktail = new Cocktail();
                        cocktail.ID = int.Parse(reader["ID"].ToString());
                        cocktail.Name = (reader["Name"] != DBNull.Value ? reader["Name"].ToString() : string.Empty);
                    }
                    return cocktail;
                }
            }
        }

        public List<Cocktail> GetCocktailsByGin(int ginId)
        {
            //List<Cocktail> Cocktails = new List<Cocktail>;

            //using (SqlConnection con = new SqlConnection(this.ConnectionString))
            //{
            //    con.Open();
            //    using (SqlCommand command = new SqlCommand())
            //    {
            //        string sSQL = "SELECT Cocktail.Id, Cocktail.Name, u.Area1, u.Area2, u.Email, r.Name FROM dbo.AspNetUsers as u INNER JOIN dbo.AspNetUserRoles as ur ON u.Id = ur.UserId INNER JOIN dbo.AspNetRoles as r ON ur.RoleId = r.Id WHERE u.Id = @UserId";

            //        command.CommandText = sSQL;
            //        command.Parameters.AddWithValue("@cocktailID", cocktailID);
            //        command.Connection = con;

            //        SqlDataReader reader = command.ExecuteReader();
            //        while (reader.Read())
            //        {
            //            Cocktail c = new Cocktail();

            //            c.ID = int.Parse(reader["ID"].ToString());
            //            c.Name = (reader["Name"] != DBNull.Value ? reader["Name"].ToString() : string.Empty);
            //        }
            //    }
            //}
            return null;
        }
    }
}
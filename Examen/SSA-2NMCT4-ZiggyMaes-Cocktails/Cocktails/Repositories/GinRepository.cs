using Cocktails.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Cocktails.Repositories
{
    public class GinRepository
    {
        private string ConnectionString { get; set; }
        public GinRepository()
        {
            this.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public List<Gin> GetGins()
        {
            List<Gin> gins = new List<Gin>();
            using (SqlConnection con = new SqlConnection(this.ConnectionString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "SELECT * FROM Gins";
                    command.Connection = con;

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Gin gin = new Gin();
                        gin.ID = int.Parse(reader["ID"].ToString());
                        gin.Name = (reader["Name"] != DBNull.Value ? reader["Name"].ToString() : string.Empty);
                        gins.Add(gin);
                    }
                }
            }
            return gins;
        }

        public Gin GetGinById(int id)
        {
            Gin gin = null;
            using (SqlConnection con = new SqlConnection(this.ConnectionString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "SELECT * FROM Gins WHERE Id = @id";
                    command.Connection = con;
                    command.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        gin = new Gin();
                        gin.ID = int.Parse(reader["ID"].ToString());
                        gin.Name = (reader["Description"] != DBNull.Value ? reader["Name"].ToString() : string.Empty);
                        gin.Description = (reader["Description"] != DBNull.Value ? reader["Description"].ToString() : string.Empty);
                        gin.Price = double.Parse(reader["Price"] != DBNull.Value ? reader["Price"].ToString() : "0");
                    }
                }
            }
            return gin;
        }
    }
}

//Public Shared Function getHotelsBySearch(ByVal sSearch As String) As List(Of Hotel)
//        Dim sSQL As String
//        sSQL = "SELECT * FROM Hotel "
//        sSQL &= " WHERE HotelNaam Like @HotelNaam "
//        sSQL &= " OR Locatie  Like @Locatie "


//        Dim oPar1 As DbParameter = Database.GetParameter("@HotelNaam", "%" & sSearch & "%")
//        Dim oPar2 As DbParameter = Database.GetParameter("@Locatie", "%" & sSearch & "%")
//        Return _GetList(sSQL, oPar1, oPar2)
//    End Function

// sSQL &= "ORDER BY PeriodeStart DESC"
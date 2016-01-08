using Cocktails.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Cocktails.Repositories
{
    public class IngredientRepository
    {

        private string ConnectionString { get; set; }

        public IngredientRepository()
        {
            this.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public List<Ingredient> GetIngredientsByCocktail(int cocktailID)
        {
            return null;
        }

    }
}
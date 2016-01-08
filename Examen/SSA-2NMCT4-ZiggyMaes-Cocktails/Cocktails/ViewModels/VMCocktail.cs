using Cocktails.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cocktails.ViewModels
{
    public class VMCocktail
    {
        public VMCocktail()
        {
            Ingredients = new List<Ingredient>();
        }

        public Cocktail CurrentCocktail { get; set; }
        public List<Ingredient> Ingredients { get; set; }
    }
}

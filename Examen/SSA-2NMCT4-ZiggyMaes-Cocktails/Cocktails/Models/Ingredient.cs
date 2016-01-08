using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cocktails.Models
{
    public class Ingredient
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public double Quantity { get; set; }
    }
}
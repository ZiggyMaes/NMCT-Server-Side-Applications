using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceptenApp.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}

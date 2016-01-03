using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public int Stars { get; set; }
        public int MessageId { get; set; }
        public int UserId { get; set; }
    }
}

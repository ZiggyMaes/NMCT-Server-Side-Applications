using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime TimePosted { get; set; }
        public int ParentId { get; set; }
        public bool Visible { get; set; }
        public int AreaId { get; set; }
        public int UserId { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Models
{
    public class Message
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Content is required"), Display(Name = "Content"), DataType(DataType.MultilineText)]
        public string Body { get; set; }
        public DateTime TimePosted { get; set; }
        public int ParentId { get; set; }
        [DefaultValue(true)]
        public bool Visible { get; set; }
        public int AreaId { get; set; }
        public int UserId { get; set; }
        public int PostCount { get; set; }
    }
}

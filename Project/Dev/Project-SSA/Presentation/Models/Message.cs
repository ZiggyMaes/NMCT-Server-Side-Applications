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
        [ReadOnly(true)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Content is required"), Display(Name = "Content"), DataType(DataType.MultilineText)]
        public string Body { get; set; }
        [ReadOnly(true)]
        public DateTime TimePosted { get; set; }
        [ReadOnly(true)]
        public int ParentId { get; set; }
        [ReadOnly(true)]
        public bool Visible { get; set; }
        [ReadOnly(true)]
        public int AreaId { get; set; }
        [ReadOnly(true)]
        public int UserId { get; set; }
        [ReadOnly(true)]
        public int PostCount { get; set; }
    }
}

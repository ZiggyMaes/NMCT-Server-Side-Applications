using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02.Models
{
    public class Contact
    {
        [Required(ErrorMessage = "Dit moet worden ingevuld")]
        [MaxLength(15)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Dit moet worden ingevuld")]
        [MaxLength(15)]
        public string Question { get; set; }

        [Required(ErrorMessage = "Dit moet worden ingevuld")]
        [MaxLength(15)]
        public string Title { get; set; }
    }
}

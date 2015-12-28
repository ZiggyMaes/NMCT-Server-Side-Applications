using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public DateTime RegistrationDateTime { get; set; }
        public DateTime LastLogin { get; set; }
        public string LastKnowIP { get; set; }
        public bool SuperUser { get; set; }
        public bool Admin { get; set; }
        public bool Blocked { get; set; }
        public bool Inactive { get; set; }
        public int PrimaryArea { get; set; }
        public int SecondaryArea { get; set; }

    }
}

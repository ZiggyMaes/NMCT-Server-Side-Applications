using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oef2.Models
{
    public class Register
    {
        public string Name { get; set; }
        public string FirstName { get; set; }
        public short Age { get; set; }
        public string Slot1 { get; set; }
        public string Slot2 { get; set; }
        public string Slot3 { get; set; }
        public string[] Accessoires { get; set; }
        public string Organization { get; set; }
        public bool AttendingClosingParty { get; set; }
    }
}

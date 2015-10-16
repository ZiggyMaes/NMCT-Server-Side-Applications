using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oef2.Models
{
    public class Session
    {
        public int Id { get; set; }
        public int Slot { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

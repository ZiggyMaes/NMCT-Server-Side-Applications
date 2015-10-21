using Oef2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Oef2.PresentationModels
{
    public class PMRegister
    {
        public List<Session> Slot1 { get; set; }
        public List<Session> Slot2 { get; set; }
        public List<Session> Slot3 { get; set; }
        public List<Organization> Organizations { get; set; }
    }
}

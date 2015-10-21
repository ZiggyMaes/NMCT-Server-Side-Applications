using Oef2.PresentationModels;
using Oef2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Oef2.Controllers
{
    public class RegisterController : Controller
    {
        public ActionResult New()
        {
            PMRegister pm = new PMRegister();
            pm.Slot1=Data.GetSessions(1);
            pm.Slot2=Data.GetSessions(2);
            pm.Slot3=Data.GetSessions(3);
            pm.Organizations=Data.GetOrganizations();
            return View();
        }
    }
}
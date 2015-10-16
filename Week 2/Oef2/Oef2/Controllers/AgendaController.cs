using Oef2.Models;
using Oef2.PresentationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Oef2.Controllers
{
    public class AgendaController : Controller
    {
        public ActionResult Show()
        {
            PMAgenda pm = new PMAgenda();
            pm.Slot1=Data.GetSessions(1);
            pm.Slot2=Data.GetSessions(2);
            pm.Slot3=Data.GetSessions(3);

            return View(pm);
        }

        public ActionResult Detail(int? id)
        {
            Session s = Data.FindSession(id.Value);
            if(!id.HasValue || s == null) return RedirectToAction("Show");

            return View(s);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Presentation.Repositories;
using Presentation.Models;

namespace Presentation.Controllers
{
    public class ForumController : Controller
    {
        // GET: Forum
        public ActionResult Index(int? AreaId)
        {
            if (AreaId == null) return RedirectToAction("Index", "Home");

            Area CurrentArea = AreaRepository.GetAreaInfo(Convert.ToInt32(AreaId));
            List<Message> Threads = ForumRepository.GetThreads(Convert.ToInt32(AreaId));

            ViewBag.CurrentArea = CurrentArea.Title;
            return View(Threads);
        }
    }
}
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
        [HttpGet]
        public ActionResult Index(int? AreaId)
        {
            if (AreaId == null) return RedirectToAction("Index", "Home");

            Area CurrentArea = AreaRepository.GetAreaInfo(Convert.ToInt32(AreaId));
            if (CurrentArea == null) return RedirectToAction("Index", "Home"); //if no records were returned

            List<Message> Threads = ForumRepository.GetThreads(Convert.ToInt32(AreaId));

            ViewBag.CurrentArea = CurrentArea.Title;
            ViewBag.CurrentAreaId = CurrentArea.Id;
            return View(Threads);
        }

        [HttpGet]
        public ActionResult ViewThread(int? ThreadId)
        {
            if (ThreadId == null) return RedirectToAction("Index", "Home");

            Message Thread = ForumRepository.GetMessage(Convert.ToInt32(ThreadId));
            if(Thread == null) return RedirectToAction("Index", "Home");

            return View(Thread);
        }

        [HttpGet]
        public ActionResult NewThread(int? AreaId)
        {
            if (AreaId == null) return RedirectToAction("Index", "Home");

            Area CurrentArea = AreaRepository.GetAreaInfo(Convert.ToInt32(AreaId));
            if (CurrentArea == null) return RedirectToAction("Index", "Home"); //if no records were returned

            ViewBag.AreaId = AreaId;
            return View();
        }

        [HttpPost]
        public ActionResult NewThread(Message Thread)
        {
            Thread.TimePosted = DateTime.Now;
            Thread.ParentId = -1;
            Thread.Visible = true;
            Thread.UserId = 0; // ---------> MUST CHANGE!!!!!!!!!!!!!!!

            ForumRepository.AddMessage(Thread);

            return View("ViewThread");
        }
    }
}
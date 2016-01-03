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

            List<Message> Messages = ForumRepository.GetMessages(Convert.ToInt32(ThreadId));
            if (Messages == null) return RedirectToAction("Index", "Home");

            return View(Messages);
        }
        [HttpPost]
        public ActionResult ViewThread(Message Comment)
        {
           return PostComment(Comment);
        }
        [HttpGet]
        public ActionResult NewThread(int? AreaId)
        {
            if (AreaId == null) return RedirectToAction("Index", "Home");

            Area CurrentArea = AreaRepository.GetAreaInfo(Convert.ToInt32(AreaId));
            if (CurrentArea == null) return RedirectToAction("Index", "Home"); //if no records were returned

            ViewBag.AreaId = Convert.ToInt32(AreaId);
            return View();
        }

        [HttpPost]
        public ActionResult NewThread(Message Thread)
        {
            int ThreadId = 0;

            Thread.TimePosted = DateTime.Now;
            Thread.UserId = 0; // ---------> MUST CHANGE!!!!!!!!!!!!!!!

            ThreadId = ForumRepository.AddMessage(Thread);
            ForumRepository.UpdateParentId(ThreadId);//Update the ParentId value of the thread to match the Id (this is how we differentiate threads from posts)

            return RedirectToAction("ViewThread", "Message", new { ThreadId = ThreadId });
        }
        
        [HttpGet]
        public ActionResult PostComment(int? ThreadId)
        {
            ViewBag.ThreadId = Convert.ToInt32(ThreadId);
            return View();
        }

        [HttpPost]
        public ActionResult PostComment(Message Comment)
        {
            Comment.Title = "RE";
            Comment.TimePosted = DateTime.Now;
            Comment.UserId = 0; // ---------> MUST CHANGE!!!!!!!!!!!!!!!

            ForumRepository.AddMessage(Comment);

            return View("Index");
        }
    }
}
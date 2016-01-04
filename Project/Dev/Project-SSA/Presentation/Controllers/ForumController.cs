using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Presentation.Repositories;
using Presentation.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace Presentation.Controllers
{
    public class ForumController : Controller
    {
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        [HttpGet]
        [Authorize(Roles = "Administrator, Superuser, User")]
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
        [Authorize(Roles = "Administrator, Superuser, User")]
        public ActionResult ViewThread(int? ThreadId)
        {
            if (ThreadId == null) return RedirectToAction("Index", "Home");

            List<Message> Messages = ForumRepository.GetMessages(Convert.ToInt32(ThreadId));
            if (Messages == null) return RedirectToAction("Index", "Home");

            return View(Messages);
        }
        [HttpPost]
        [Authorize(Roles = "Administrator, Superuser, User")]
        public ActionResult ViewThread(Message Comment)
        {
            return PostComment(Comment);
        }
        [HttpGet]
        public ActionResult NewThread(int? AreaId)
        {
            if (AreaId == null) return RedirectToAction("Index", "Home");
            if (!User.IsInRole("Superuser") && !User.IsInRole("Administrator")) return View("FaultyRole");

            Area CurrentArea = AreaRepository.GetAreaInfo(Convert.ToInt32(AreaId));
            if (CurrentArea == null) return RedirectToAction("Index", "Home"); //if valid area was found

            ViewBag.AreaId = Convert.ToInt32(AreaId);
            return View();
        }

        [HttpPost]
        public ActionResult NewThread(Message Thread)
        {
            if (!User.IsInRole("Superuser") && !User.IsInRole("Administrator")) return View("FaultyRole");
            int ThreadId = 0;

            Thread.TimePosted = DateTime.Now;
            Thread.UserInfo = UserRepository.GetUser(UserManager.FindById(User.Identity.GetUserId()).Id);

            ThreadId = ForumRepository.AddMessage(Thread);
            ForumRepository.UpdateParentId(ThreadId);//Update the ParentId value of the thread to match the Id (this is how we differentiate threads from posts)

            return RedirectToAction("ViewThread", new { ThreadId = ThreadId });
        }
        
        [HttpGet]
        [Authorize(Roles = "Administrator, Superuser, User")]
        public ActionResult PostComment(int? ThreadId)
        {
            ViewBag.ThreadId = Convert.ToInt32(ThreadId);
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Superuser, User")]
        public ActionResult PostComment(Message Comment)
        {
            Comment.Title = "RE";
            Comment.TimePosted = DateTime.Now;
            Comment.UserInfo = UserRepository.GetUser(UserManager.FindById(User.Identity.GetUserId()).Id);

            ForumRepository.AddMessage(Comment);

            return RedirectToAction("ViewThread", new { ThreadId = Comment.ParentId });
        }
    }
}
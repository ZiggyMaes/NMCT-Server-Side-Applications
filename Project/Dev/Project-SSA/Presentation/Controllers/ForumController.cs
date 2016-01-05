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
            if (AreaId == null || !ModelState.IsValid) return RedirectToAction("ForbiddenAction", "Error");

            Area CurrentArea = AreaRepository.GetAreaInfo(Convert.ToInt32(AreaId));
            if (CurrentArea == null) return RedirectToAction("Index", "Home"); //if no records were returned

            User CurrentUser = UserRepository.GetUser(UserManager.FindByEmail(User.Identity.Name).Id);
            if (CurrentUser.Area1 != AreaId && CurrentUser.Area2 != AreaId) return RedirectToAction("Select", "Area");

            List<Message> Threads = ForumRepository.GetThreads(Convert.ToInt32(AreaId));

            ViewBag.CurrentArea = CurrentArea.Title;
            ViewBag.CurrentAreaId = CurrentArea.Id;
            return View(Threads);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator, Superuser, User")]
        public ActionResult ViewThread(int? ThreadId)
        {
            if (ThreadId == null || !ModelState.IsValid) return RedirectToAction("ForbiddenAction", "Error");

            List<Message> Messages = ForumRepository.GetMessages(Convert.ToInt32(ThreadId));
            if (Messages == null) return RedirectToAction("Index", "Home");

            return View(Messages);
        }
        [HttpPost]
        [Authorize(Roles = "Administrator, Superuser, User")]
        public ActionResult ViewThread(Message Comment, string Title)
        {
            return PostComment(Comment);
        }
        [HttpGet]
        public ActionResult NewThread(int? AreaId)
        {
            if (AreaId == null || !ModelState.IsValid) return RedirectToAction("ForbiddenAction", "Error");
            if (!User.IsInRole("Superuser") && !User.IsInRole("Administrator")) return RedirectToAction("AccessDenied", "Error");

            Area CurrentArea = AreaRepository.GetAreaInfo(Convert.ToInt32(AreaId));
            if (CurrentArea == null) return RedirectToAction("Index", "Home"); //if no valid area was found

            ViewBag.AreaId = Convert.ToInt32(AreaId);
            return View();
        }

        [HttpPost]
        public ActionResult NewThread(Message Thread)
        {
            if (Thread == null || !ModelState.IsValid) return RedirectToAction("ForbiddenAction", "Error");
            if (!User.IsInRole("Superuser") && !User.IsInRole("Administrator")) return RedirectToAction("AccessDenied", "Error");
            int ThreadId = 0;

            Thread.TimePosted = DateTime.Now;
            Thread.UserInfo = UserRepository.GetUser(UserManager.FindByEmail(User.Identity.Name).Id);

            ThreadId = ForumRepository.AddMessage(Thread);
            ForumRepository.UpdateParentId(ThreadId);//Update the ParentId value of the thread to match the Id (this is how we differentiate threads from posts)

            return RedirectToAction("ViewThread", new { ThreadId = ThreadId, Title = Thread.Title });
        }
        
        [HttpGet]
        [Authorize(Roles = "Administrator, Superuser, User")]
        public ActionResult PostComment(int? ThreadId, string Title, string AreaId)
        {
            if (ThreadId == null || !ModelState.IsValid) return RedirectToAction("ForbiddenAction", "Error");
            ViewBag.ThreadId = Convert.ToInt32(ThreadId);
            ViewBag.Title = Title;
            ViewBag.AreaId = AreaId;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Superuser, User")]
        public ActionResult PostComment(Message Comment)
        {
            if (Comment == null || !ModelState.IsValid) return RedirectToAction("ForbiddenAction", "Error");
            Comment.Title = "RE: " + Comment.Title;
            Comment.TimePosted = DateTime.Now;
            Comment.UserInfo = UserRepository.GetUser(UserManager.FindByEmail(User.Identity.Name).Id);

            ForumRepository.AddMessage(Comment);

            if (!User.IsInRole("Administrator") && ForumRepository.GetPostcount(Comment.UserInfo.UserId) == 5) UserRepository.SetRole(Comment.UserInfo.UserId, 2);

            return RedirectToAction("ViewThread", new { ThreadId = Comment.ParentId });
        }
        [HttpPost]
        [Authorize(Roles = "Administrator, Superuser, User")]
        public ActionResult Search(string AreaId, string Query)
        {
            if (AreaId == null || !ModelState.IsValid) return RedirectToAction("ForbiddenAction", "Error");
            List<Message> Results = ForumRepository.Search(Convert.ToInt32(AreaId), Query);

            ViewBag.CurrentAreaId = AreaId;
            return View("SearchResults", Results); 
        }
        [HttpGet]
        [Authorize(Roles = "Administrator, Superuser, User")]
        public ActionResult SearchResults(List<Message> Results)
        {
            if (!ModelState.IsValid) return RedirectToAction("ForbiddenAction", "Error");
            return View(Results);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult HideThread(int? ThreadId, int? AreaId)
        {
            if (ThreadId == null || AreaId == null || !ModelState.IsValid) return RedirectToAction("ForbiddenAction", "Error");
            ForumRepository.HideMessage(Convert.ToInt32(ThreadId));
            return RedirectToAction("Index", "Forum", new { AreaId = AreaId });
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult HideMessage(int? MessageId, int? ThreadId)
        {
            if (MessageId == null || ThreadId == null || !ModelState.IsValid) return RedirectToAction("ForbiddenAction", "Error");
            ForumRepository.HideMessage(Convert.ToInt32(MessageId));
            return RedirectToAction("ViewThread", "Forum", new { ThreadId = ThreadId });
        }
    }
}
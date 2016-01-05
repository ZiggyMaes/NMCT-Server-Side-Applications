using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Presentation.Models;
using Presentation.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentation.Controllers
{
    public class AreaController : Controller
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

            User CurrentUser = UserRepository.GetUser(UserManager.FindByEmail(User.Identity.Name).Id);
            if (CurrentUser.Area1 != AreaId && CurrentUser.Area2 != AreaId) return RedirectToAction("Select");

            return View(CurrentArea);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator, Superuser, User")]
        public ActionResult Select()
        {
            List<Area> Areas = AreaRepository.GetAreas();

            User CurrentUser = UserRepository.GetUser(UserManager.FindByEmail(User.Identity.Name).Id);
            ViewBag.Area1 = CurrentUser.Area1;
            ViewBag.Area2 = CurrentUser.Area2;

            return View(Areas);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Superuser, User")]
        public ActionResult Select(String[] Areas)
        {
            int[] ReturnAreas = new int[2];

            if (Areas == null)
            {
                ReturnAreas[0] = -1;
                ReturnAreas[1] = -1;
            }
            else if (Areas.Length == 1)
            {
                ReturnAreas[0] = Convert.ToInt32(Areas[0]);
                ReturnAreas[1] = -1;
            }
            else
            {
                ReturnAreas[0] = Convert.ToInt32(Areas[0]);
                ReturnAreas[1] = Convert.ToInt32(Areas[1]);
            }

            AreaRepository.UpdateUserAreas(UserManager.FindByEmail(User.Identity.Name).Id, ReturnAreas);
            List<Area> UpdatedAreas = AreaRepository.GetAreas();
            return RedirectToAction("Select", UpdatedAreas);
        }
    }
}
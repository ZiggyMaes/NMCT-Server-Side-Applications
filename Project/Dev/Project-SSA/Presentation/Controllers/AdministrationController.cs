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
    public class AdministrationController : Controller
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
        [Authorize(Roles = "Administrator")]
        public ActionResult Statistics()
        {
            List<Statistic> Statistics = new List<Statistic>();

            Statistics.Add(new Statistic("New Threads (24h)", StatisticRepository.GetNewThreadCount().ToString()));
            Statistics.Add(new Statistic("New Posts (24h)", StatisticRepository.GetNewPostCount().ToString()));
            Statistics.Add(new Statistic("Total Threadcount", StatisticRepository.GetTotalThreadCount().ToString()));
            Statistics.Add(new Statistic("Total Postcount", StatisticRepository.GetTotalPostCount().ToString()));


            return View(Statistics);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Manage()
        {
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult PurgeOldUsers()
        {
            //AdministrationRepository.PurgeUsers(); /* Was not able to implement in time */
            return RedirectToAction("Manage");
        }
    }
}
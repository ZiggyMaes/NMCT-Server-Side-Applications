using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _01_Intro.Controllers
{
    public class DateController : Controller
    {
        //// GET: Date
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult Today()
        {
            ViewBag.Today = DateTime.Now.ToString();
            //return null;
             return View();
        }

        public ActionResult Tomorrow()
        {
            ViewBag.Tomorrow = DateTime.Now.AddDays(1).ToString();
            return View("Today");
        }

        public ActionResult Yesterday()
        {
            ViewBag.Tomorrow = DateTime.Now.AddDays(-1).ToString();
            return View("Today");
        }
    }
}
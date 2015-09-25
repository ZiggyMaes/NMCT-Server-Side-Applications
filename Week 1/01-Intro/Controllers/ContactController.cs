using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _01_Intro.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Save(string title, string info)
        {
            if (String.IsNullOrEmpty(title) || String.IsNullOrEmpty(info)) return View("Error");
            ViewBag.Title = title;
            ViewBag.Info = info;
            return View();
        }
    }
}
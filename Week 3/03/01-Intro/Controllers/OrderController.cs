using _01_Intro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _01_Intro.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        private List<Tablet> Tablets = new List<Tablet>();

        public OrderController()
        {
            Tablets.Add(new Tablet() { Id = "1", Name = "iPad" });
            Tablets.Add(new Tablet() { Id = "2", Name = "Nexus" });
            Tablets.Add(new Tablet() { Id = "3", Name = "Surface" });
        }

        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Next(int? step, FormCollection frm)
        {
            if (step == 0) return RedirectToAction("New"); //VERGEET RETURN ZEKER NIET

            switch(step)
            {
                case 1:
                    ViewBag.FirstName = Request.Form["firstName"].ToString();
                    ViewBag.LastName = Request.Form["lastName"].ToString();
                    ViewBag.Company = Request.Form["company"].ToString();

                    ViewBag.Tablets = Tablets;

                    return View("Step1");

                case 2:
                    ViewBag.FirstName = Request.Form["firstName"].ToString();
                    ViewBag.LastName = Request.Form["lastName"].ToString();
                    ViewBag.Company = Request.Form["company"].ToString();

                    string tabletID = Request.Form["tablet"];

                    //Lambda =>
                    ViewBag.Tablet = Tablets.Find(tablet => tablet.Id == tabletID);

                    ViewBag.Toebehoren = Request.Form["toebehoren[]"];

                    return View("Step2");

                default: return RedirectToAction("New");
            }
        }
    }
}
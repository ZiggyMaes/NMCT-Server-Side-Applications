using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _01_Intro.Controllers
{
    public class TimeController : Controller
    {
        // GET: Time
        public ActionResult WhatTime(int? uur, int? min, int sec=0) //? zorgt ervoor dat de argumenten null mogen zijn
        {
            if (uur == null || min == null) return View("Error");
            ViewBag.Timestamp = string.Format("{0}:{1}:{2}", uur, min, sec);

            //var uur = Request.QueryString["uur"];
            return View();
        }
    }
}
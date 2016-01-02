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
        [HttpGet]
        public ActionResult Index(int? AreaId)
        {
            if(AreaId == null) return RedirectToAction("Index", "Home");

            Area CurrentArea = AreaRepository.GetAreaInfo(Convert.ToInt32(AreaId));
            if(CurrentArea == null) return RedirectToAction("Index", "Home"); //if no records were returned

            return View(CurrentArea);
        }
    }
}
using _02.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _02.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Create()
        {
            return View(new Contact());
        }

        [HttpPost]
        public ActionResult Create(Contact contact)
        {
            if(ModelState.IsValid) return View("Details", contact);

            return View("Create");
        }
    }
}
using _01_Intro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _01_Intro.Controllers
{
    public class ServerVariablesController : Controller
    {
        // GET: ServerVariables
        public ActionResult ServerVariables()
        {
            List<ServerVariable> vars = new List<ServerVariable>();
            foreach (var key in Request.ServerVariables.AllKeys)
            {
                vars.Add(new ServerVariable()
                {
                    Key = key.ToString(),
                    Value = Request.ServerVariables[key.ToString()]
                });
            }

            ViewBag.Result = vars;
            return View();
        }
    }
}
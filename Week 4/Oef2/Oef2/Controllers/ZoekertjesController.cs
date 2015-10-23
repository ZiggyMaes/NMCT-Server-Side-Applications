using Oef2.PresentationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zoekertjes.WebApp.DataAccess;
using Zoekertjes.WebApp.Models;

namespace Oef2.Controllers
{
    public class ZoekertjesController : Controller
    {
        // GET: Zoekertjes
        public ActionResult Index()
        {
            List<Zoekertje> alleZoekertjes = new List<Zoekertje>();
            alleZoekertjes = Data.GetZoekertjes();

            return View(alleZoekertjes);
        }

        public ActionResult Details(int? id)
        {
            if (!id.HasValue) return RedirectToAction("Index");

            Zoekertje zoekertje = Data.GetZoekertje(id.Value);

            Categorie categorie = Data.GetCategorie(zoekertje.CategorieId);
            ViewBag.Categorie = categorie.Naam;

            Locatie locatie = Data.GetLocatie(zoekertje.LocatieId);
            ViewBag.Locatie = locatie.Naam;

            return View(zoekertje);
        }

        public  ActionResult New()
        {
            PMNewZoekertje zoekertje = new PMNewZoekertje();
            zoekertje.Categories = new SelectList(Data.GetCategories(), "Id", "Naam");
            zoekertje.Locaties = new SelectList(Data.GetLocaties(), "Id", "Naam");
            return View(zoekertje);
        }
    }
}
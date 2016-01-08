using Cocktails.Models;
using Cocktails.Repositories;
using Cocktails.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cocktails.Controllers
{
    public class GinController : Controller
    {
        GinRepository GinData = new GinRepository();
        CocktailRepository CocktailData = new CocktailRepository();

        // GET: Gin
        public ActionResult Index()
        {
            GinRepository Repo = new GinRepository();
            List<Gin> AllGins = Repo.GetGins();

            return View(AllGins);
        }

        public ActionResult Info(int? GinId)
        {
            if (GinId == null || !ModelState.IsValid) return RedirectToAction("Index", "Gin");

            Gin CurrentGin = GinData.GetGinById(Convert.ToInt32(GinId));
            ViewBag.GinImageName = CurrentGin.Name.Replace(" ", "") + ".jpg";

            return View(CurrentGin);
        }
        [HttpGet]
        public ActionResult Order(int? GinId)
        {
            if (GinId == null || !ModelState.IsValid) return RedirectToAction("Index", "Gin");

            List <VMCocktail> CocktailList = new List<VMCocktail>();
            List<Ingredient> IngredientList = new List<Ingredient>();

            Cocktail c = new Cocktail();
            Ingredient i = new Ingredient();
            VMCocktail vm = new VMCocktail();

            c.ID = 0;
            c.Name = "Cocktail name";

            i.ID = 0;
            i.Name = "Ingredient name";
            i.Quantity = 250;

            IngredientList.Add(i);

            vm.CurrentCocktail = c;
            vm.Ingredients = IngredientList;

            CocktailList.Add(vm);

            ViewBag.Gin = GinData.GetGinById(Convert.ToInt32(GinId));

            return View(CocktailList);
        }
        [HttpPost]
        public ActionResult Order(int? GinId, string[] SelectedCocktails)
        {
            //SelectedCocktails --> array van cocktail IDs
            List<Cocktail> OrderedCocktails = new List<Cocktail>();

            int i = 0;
            for(i=0;i<=SelectedCocktails.Length - 1;i++)
            {
                Cocktail c = CocktailData.GetCocktailByID(Convert.ToInt32(SelectedCocktails[i]));
                //OrderedCocktails.Add();
                i++;
            }

            ViewBag.Gin = GinData.GetGinById(Convert.ToInt32(GinId));
            return View("OrderSuccess", OrderedCocktails);
        }
    }
}
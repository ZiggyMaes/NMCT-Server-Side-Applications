using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zoekertjes.WebApp.Models;

namespace Zoekertjes.WebApp.DataAccess
{
    public class Data
    {
        private static List<Zoekertje> zoekertjes = new List<Zoekertje>();

        static Data()
        {
            zoekertjes.Add(new Zoekertje() { Id = 1, CategorieId = 1, ContacteerViaEMail = true, ContacteerViaTelefoon = false, Email = "dieter@howest.", LocatieId = 3, Naam = "Dieter De Preester", Omschrijving = "Ik ben opzoek naar een oude ipad", Prijs = 120M, Telefoon = "", Titel = "iPad gezocht" });
            zoekertjes.Add(new Zoekertje() { Id = 2, CategorieId = 3, ContacteerViaEMail = true, ContacteerViaTelefoon = true, Email = "wijndrinker@wijn.be.", LocatieId = 4, Naam = "Steven Martnes", Omschrijving = "Iemand nog een oude fles wijn liggen uit 1988", Prijs = 10M, Telefoon = "0555454545", Titel = "Oude wijn" });
        }

        public static List<Locatie> GetLocaties()
        {
            List<Locatie> locaties = new List<Locatie>();
            locaties.Add(new Locatie() { Id = 1, Naam = "Kortrijk" });
            locaties.Add(new Locatie() { Id = 2, Naam = "Heule" });
            locaties.Add(new Locatie() { Id = 3, Naam = "Gent" });
            locaties.Add(new Locatie() { Id = 4, Naam = "Brugge" });
            return locaties;
        }

        public static List<Categorie> GetCategories()
        {
            List<Categorie> locaties = new List<Categorie>();
            locaties.Add(new Categorie() { Id = 1, Naam = "IT" });
            locaties.Add(new Categorie() { Id = 2, Naam = "Speelgoed" });
            locaties.Add(new Categorie() { Id = 3, Naam = "Eten & Drinken" });
            locaties.Add(new Categorie() { Id = 4, Naam = "Kleren" });
            return locaties;
        }

        public static List<DeleteReden> GetDeleteRedens()
        {
            List<DeleteReden> redens = new List<DeleteReden>();
            redens.Add(new DeleteReden() { Id = 1, Naam = "Reeds gevonden" });
            redens.Add(new DeleteReden() { Id = 2, Naam = "Geen interesse meer" });
            redens.Add(new DeleteReden() { Id = 3, Naam = "Aanbod is te duur" });
            return redens;
        }
        public static List<Zoekertje> GetZoekertjes()
        {
            return zoekertjes;
        }

        public static Locatie GetLocatie(int locatieId)
        {
            return GetLocaties().Find(l => l.Id == locatieId);
        }

        public static Categorie GetCategorie(int categorieId)
        {
            return GetCategories().Find(c => c.Id == categorieId);
        }

        public static Zoekertje GetZoekertje(int id)
        {
            return zoekertjes.Find(z => z.Id == id);
        }

        public static void AddZoekertje(Zoekertje newZoekertje)
        {
            newZoekertje.Id = zoekertjes.Count + 1;
            zoekertjes.Add(newZoekertje);
        }

        public static void DeleteZoekertjes(int p)
        {
            zoekertjes.Remove(GetZoekertje(p));
        }
    }
}
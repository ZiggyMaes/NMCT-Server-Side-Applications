using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Zoekertjes.WebApp.Models;

namespace Oef2.PresentationModels
{
    public class PMNewZoekertje
    {
        public Zoekertje NewZoekertje { get; set; }
        public SelectList Categories { get; set; }
        public SelectList Locaties { get; set; }
    }
}

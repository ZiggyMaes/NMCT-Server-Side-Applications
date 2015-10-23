using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zoekertjes.WebApp.Models
{
    public class Zoekertje
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [DisplayName("Korte omschrijving (max 100 kar.)")]
        [MaxLengthAttribute(100)]
        public string Titel { get; set; }
        [Required]
        [DisplayName("Wat zoek ik")]
        public string Omschrijving { get; set; }
        [Required]
        public string Naam { get; set; }
        [Required]
        public string Telefoon { get; set; }
        [Required]
        [EmailAddress]
        [DisplayName("E-Mail")]
        public string Email { get; set; }
        [Required]
        [DisplayName("Max. prijs die ik wens te betalen")]
        public decimal Prijs { get; set; }
        [Required]
        [DisplayName("Wat is de categorie van het zoekertje")]
        public int CategorieId { get; set; }
        [Required]
        [DisplayName("Waar kan ik het zoekertje afhalen")]
        public int LocatieId { get; set; }
        [DisplayName("Contacteer me via telefoon")]
        public bool ContacteerViaTelefoon { get; set; }
        [DisplayName("Contacteer me via e-mail")]
        public bool ContacteerViaEMail { get; set; }

        public override string ToString()
        {
            return string.Format("{0} €{1}", Titel, Prijs);
        }
    }
}
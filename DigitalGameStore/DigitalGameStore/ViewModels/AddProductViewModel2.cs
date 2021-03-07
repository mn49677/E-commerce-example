using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DigitalGameStore.Models;
using Microsoft.AspNetCore.Http;

namespace DigitalGameStore.ViewModels
{
    public class AddProductViewModel2
    {
        public int ProductId { get; set; }
        [Display(Name = "Slika proizvoda")]
        public IFormFile Picture { get; set; }
        [Required]
        [Display(Name = "Opis prozvoda")]
        public string ProductDescription { get; set; }
        [Required]
        [Display(Name = "Vrsta proizvoda")]
        public int ProductClassId { get; set; }
        [Required]
        [Display(Name = "Ime proizvoda")]
        public string ProductName { get; set; }
        [Required]
        [Display(Name = "Cijena proizvoda")]
        public decimal? ProductPrice { get; set; }

        public IEnumerable<ProductClass> ProductClasses { get; set; }
    }
}

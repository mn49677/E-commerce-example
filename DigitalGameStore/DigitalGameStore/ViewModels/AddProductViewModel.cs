using System;
using System.Collections.Generic;
using DigitalGameStore.Models;
using Microsoft.AspNetCore.Http;

namespace DigitalGameStore.ViewModels
{
    public class AddProductViewModel
    {
        public Product Product { get; set; }
        public IEnumerable<ProductClass> ProductClasses { get; set; }
        public IFormFile Picture { get; set; }
    }
}

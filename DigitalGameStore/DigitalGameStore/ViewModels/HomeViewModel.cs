using System;
using System.Collections.Generic;
using DigitalGameStore.Models;
namespace DigitalGameStore.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Product> Products { get; }

        public HomeViewModel(IEnumerable<Product> Products)
        {
            this.Products = Products;
        }
        public string TruncDescription(int THRESHOLD, string myStr)
        {
            return (myStr.Length > THRESHOLD) ? myStr.Substring(0, THRESHOLD) + "..." : myStr;
        }
    }
}

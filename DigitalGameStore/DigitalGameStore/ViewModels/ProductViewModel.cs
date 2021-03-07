using System.Collections.Generic;
using DigitalGameStore.Models;

namespace DigitalGameStore.ViewModels
{
    public class ProductViewModel
    {
        public Product Product { get; set; }
        public Review Review { get; set; }
        public double ReviewMark { get; set; }
    }
}

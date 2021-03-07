using System;
using System.Collections.Generic;

namespace DigitalGameStore.Models
{
    public partial class ProductClass
    {
        public ProductClass()
        {
            Product = new HashSet<Product>();
        }

        public int ProductClassId { get; set; }
        public string ProductClassName { get; set; }

        public virtual ICollection<Product> Product { get; set; }
    }
}

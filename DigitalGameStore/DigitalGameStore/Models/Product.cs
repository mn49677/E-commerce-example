using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DigitalGameStore.Models
{
    public partial class Product
    {
        public Product()
        {
            ProductOrder = new HashSet<ProductOrder>();
            Review = new HashSet<Review>();
        }
        public int ProductId { get; set; }
        [Required]
        public string ProductDescription { get; set; }
        public string ProductImgUrl { get; set; }
        [Required]
        public int ProductClassId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public decimal? ProductPrice { get; set; }

        public virtual ProductClass ProductClass { get; set; }
        public virtual ICollection<ProductOrder> ProductOrder { get; set; }
        public virtual ICollection<Review> Review { get; set; }
    }
}

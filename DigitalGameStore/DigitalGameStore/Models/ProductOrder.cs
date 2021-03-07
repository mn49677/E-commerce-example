using System;
using System.Collections.Generic;

namespace DigitalGameStore.Models
{
    public partial class ProductOrder
    {
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int ProductOrderId { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}

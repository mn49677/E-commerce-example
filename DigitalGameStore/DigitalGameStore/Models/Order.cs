using System;
using System.Collections.Generic;

namespace DigitalGameStore.Models
{
    public partial class Order
    {
        public Order()
        {
            ProductOrder = new HashSet<ProductOrder>();
        }

        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderDetails { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<ProductOrder> ProductOrder { get; set; }
    }
}

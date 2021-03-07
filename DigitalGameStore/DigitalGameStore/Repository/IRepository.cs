using System;
using System.Collections.Generic;
using System.Linq;

namespace DigitalGameStore.Models
{
    public interface IRepository
    {
        public IQueryable<Product> Products { get; }
        public IQueryable<CreditCard> CreditCard { get; }
        public IQueryable<Address> Addresses { get; }
        public IQueryable<Order> Orders { get; }
        public IQueryable<ProductClass> ProductClasses { get; }
        public IQueryable<ProductOrder> ProductOrders { get; }
        public IQueryable<Review> Reviews { get; }
        public IQueryable<User> Users { get; }

        public ris_dbContext getContext();
    }
}

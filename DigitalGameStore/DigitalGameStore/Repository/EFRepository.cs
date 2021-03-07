using System;
using System.Linq;

namespace DigitalGameStore.Models
{
    public class EFRepository: IRepository
    {
        ris_dbContext context;
        public EFRepository(ris_dbContext context)
        {
            this.context = context;
        }

        public IQueryable<Product> Products => context.Product;

        public IQueryable<CreditCard> CreditCard => context.CreditCard;

        public IQueryable<Address> Addresses => context.Address;

        public IQueryable<Order> Orders => context.Order;

        public IQueryable<ProductClass> ProductClasses => context.ProductClass;

        public IQueryable<ProductOrder> ProductOrders => context.ProductOrder;

        public IQueryable<Review> Reviews => context.Review;

        public IQueryable<User> Users => context.User;

        public ris_dbContext getContext()
        {
            return this.context;
        }
    }
}

using System;
using System.Collections.Generic;

namespace DigitalGameStore.Models
{
    public partial class User
    {
        public User()
        {
            CreditCard = new HashSet<CreditCard>();
            Order = new HashSet<Order>();
            Review = new HashSet<Review>();
        }

        public int UserId { get; set; }
        public string Username { get; set; }
        public int? CreditCardId { get; set; }
        public bool UserAdmin { get; set; }
        public int AddressId { get; set; }

        public virtual Address Address { get; set; }
        public virtual ICollection<CreditCard> CreditCard { get; set; }
        public virtual ICollection<Order> Order { get; set; }
        public virtual ICollection<Review> Review { get; set; }
    }
}

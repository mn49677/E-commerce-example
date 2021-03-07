using System;
using System.Collections.Generic;

namespace DigitalGameStore.Models
{
    public partial class Address
    {
        public Address()
        {
            User = new HashSet<User>();
        }

        public int AddressId { get; set; }
        public int PostalCode { get; set; }
        public string City { get; set; }
        public string Street { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}

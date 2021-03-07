using System;
using System.Collections.Generic;

namespace DigitalGameStore.Models
{
    public partial class CreditCard
    {
        public int CreditCardId { get; set; }
        public string CreditCardNumber { get; set; }
        public short CreditCardExpYear { get; set; }
        public short CreditCardExpMonth { get; set; }
        public short CreditCardCvv { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}

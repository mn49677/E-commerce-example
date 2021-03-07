using System;
using System.Collections.Generic;

namespace DigitalGameStore.Models
{
    public partial class Review
    {
        public int ReviewEvaluation { get; set; }
        public string ReviewComment { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string ReviewId { get; set; }

        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
    }
}

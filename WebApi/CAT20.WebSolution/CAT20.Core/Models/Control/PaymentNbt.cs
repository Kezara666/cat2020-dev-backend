using System;
using System.Collections.Generic;

namespace CAT20.Core.Models.Control
{
    public partial class PaymentNbt
    {
        public int Id { get; set; }
        public string Code { get; set; } 
        public string Name { get; set; }
        public decimal AmountPercentage { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
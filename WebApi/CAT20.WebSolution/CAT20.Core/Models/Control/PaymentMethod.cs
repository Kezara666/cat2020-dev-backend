using System;
using System.Collections.Generic;

namespace CAT20.Core.Models.Control
{
    public partial class PaymentMethod
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public sbyte Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
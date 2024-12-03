using System;
using System.Collections.Generic;

namespace CAT20.Core.Models.TradeTax
{
    public partial class BusinessSubNature
    {
        public int? ID { get; set; }
        public string BusinessSubNatureName { get; set; }
        public int BusinessSubNatureStatus { get; set; }
        public int? SabhaID { get; set; }
        public int BusinessNatureID { get; set; }
        public decimal OtherCharges { get; set; }
        public int ActiveStatus { get; set; }
        public decimal TaxAmount { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? CreatedAt { get; set; } // Change data type to DateTime
        public DateTime? UpdatedAt { get; set; }

        public virtual BusinessNature businessNature { get; set; }
    }
}
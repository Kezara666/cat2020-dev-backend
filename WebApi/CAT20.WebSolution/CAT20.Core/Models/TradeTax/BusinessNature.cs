using System;
using System.Collections.Generic;

namespace CAT20.Core.Models.TradeTax
{
    public partial class BusinessNature
    {
        public BusinessNature()
        {
            businessSubNatures = new HashSet<BusinessSubNature>();
        }

        public int? ID { get; set; }
        public string BusinessNatureName { get; set; }
        public int? ActiveStatus { get; set; }
        public int? SabhaID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<BusinessSubNature> businessSubNatures { get; set; }
    }
}
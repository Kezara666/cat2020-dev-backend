using CAT20.Core.Models.Vote;
using System;
using System.Collections.Generic;

namespace CAT20.WebApi.Resources.TradeTax
{
    public partial class BusinessNatureResource
    {
        public int? ID { get; set; }
        public string BusinessNatureName { get; set; }
        public int? ActiveStatus { get; set; }
        public int? SabhaID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        public virtual ICollection<BusinessSubNatureResource>? businessSubNatures { get; set; }
    }
}
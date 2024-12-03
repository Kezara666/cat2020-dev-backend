using CAT20.Core.Models.Vote;
using System;
using System.Collections.Generic;

namespace CAT20.WebApi.Resources.TradeTax
{
    public partial class TaxValueResource
    {
        public int ID { get; set; }
        public int ActiveStatus { get; set; }
        public int? SabhaID { get; set; }
        public int TaxTypeID { get; set; }
        public decimal AnnualValueMinimum { get; set; }
        public decimal AnnualValueMaximum { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TaxValueStatus { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
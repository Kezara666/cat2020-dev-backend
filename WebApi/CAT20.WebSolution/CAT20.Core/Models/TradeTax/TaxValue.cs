using CAT20.Core.Models.Control;
using System;
using System.Collections.Generic;

namespace CAT20.Core.Models.TradeTax
{
    public partial class TaxValue
    {
        public int ID { get; set; }
        public int ActiveStatus { get; set; }
        public int? SabhaID { get; set; }
        public int TaxTypeID { get; set; }
        public decimal AnnualValueMinimum { get; set; }
        public decimal AnnualValueMaximum { get; set; }
        public decimal TaxAmount { get; set; }
        public int TaxValueStatus { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.HelperModels
{
    public class HShopRentalCalculatorSummary
    {
        public decimal PayingAmount_lastYear_fine { get; set; } = 0;
        public decimal PayingAmount_lastYear_arreas { get; set; } = 0;

        //#2
        public decimal PayingAmount_thisYear_fine { get; set; } = 0;
        public decimal PayingAmount_thisYear_arreas { get; set; } = 0;

        //#3
        public decimal PayingAmount_serviceChargeArreas { get; set; } = 0;

        //#4
        public decimal PayingAmount_rental { get; set; } = 0;

        //#5
        public decimal PayingAmount_serviceCharge { get; set; } = 0;

        //#6
        public decimal Next_overpayment { get; set; } = 0;
    }
}

using CAT20.Core.Models.ShopRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.HelperModels
{
    public class HShopRentalCalculatorRowDetails
    {
        public ShopRentalBalance ShopRentalBalanceRow { get; set; } = new ShopRentalBalance();

        public bool IsRowFineCovered { get; set; } = false;
        public bool IsRowArreasCovered { get; set; } = false;
        public bool IsRowServiceChargeArreasCovered { get; set; } = false;
        public bool IsRowRentalCovered { get; set; } = false;
        public bool IsRowServiceChargeCovered { get; set; } = false;
        public bool IsRowCovered { get; set; } = false;
    }
}

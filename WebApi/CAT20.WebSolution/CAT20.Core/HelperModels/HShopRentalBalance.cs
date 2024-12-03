using CAT20.Core.Models.ShopRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.HelperModels
{
    public class HShopRentalBalance
    {
        public int PropertyId { get; set; }

        public int ShopId { get; set; }

        public decimal? LastYearFine { get; set; } = 0;
        public decimal? LastYearArreas { get; set; } = 0;
        public decimal? ThisYearFine { get; set; } = 0;
        public decimal? ThisYearArreas { get; set; } = 0;
        public decimal? ServiceChargeArreas { get; set; } = 0;
        public decimal? CurrentServiceCharge { get; set; } = 0;
        public decimal? CurrentRental { get; set; } = 0;
        public decimal? OverPayment { get; set; } = 0;
    }
}

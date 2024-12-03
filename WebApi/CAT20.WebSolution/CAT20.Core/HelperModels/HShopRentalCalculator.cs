using CAT20.Core.Models.ShopRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.HelperModels
{
    public class HShopRentalCalculator
    {
        public List<HShopRentalCalculatorRowDetails> ShopRentalCalculatorRowDetails { get; set; } = new List<HShopRentalCalculatorRowDetails>();

        public HShopRentalCalculatorSummary ShopRentalCalculatorSummary { get; set; } = new HShopRentalCalculatorSummary();
    }
}

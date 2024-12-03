using CAT20.Core.Models.ShopRental;

namespace CAT20.WebApi.Resources.ShopRental
{
    public class ShopRentalPayableBalanceResource
    {
        public int Id { get; set; } //primary key

        public int? PropertyId { get; set; } //Foreign Key field

        public int? ShopId { get; set; } //Foreign Key field

        public int Year { get; set; }

        public int Month { get; set; }

        public decimal? CurrentRentalAmount { get; set; }

        public decimal? LastYearTotalArreas { get; set; }

        public decimal? ThisYearTotalArreas { get; set; }

        public decimal? LastYearTotalFine { get; set; } 

        public decimal? ThisYearTotalFine { get; set; } //Total fine up to last month

        public decimal? OverPaymentAmount { get; set; }

        //Mapping 1(property): many (balances)
        public virtual PropertyResource? Property { get; set; }

        //Mapping 1(shop): many (balances)
        public virtual ShopResource? Shop { get; set; }
    }
}

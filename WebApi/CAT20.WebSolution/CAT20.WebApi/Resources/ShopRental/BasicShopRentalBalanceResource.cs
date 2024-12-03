using CAT20.WebApi.Resources.Control;
using Microsoft.EntityFrameworkCore;

namespace CAT20.WebApi.Resources.ShopRental
{
    public partial class BasicShopRentalBalanceResource
    {
        public int? Id { get; set; } //primary key

        public int? PropertyId { get; set; } //Foreign Key field

        public int? ShopId { get; set; } //Foreign Key field

        public int Year { get; set; }

        public int Month { get; set; }

        public DateOnly FromDate { get; set; }

        public DateOnly? ToDate { get; set; }

        public DateOnly BillProcessDate { get; set; }

        public decimal? ArrearsAmount { get; set; }

        public decimal? PaidArrearsAmount { get; set; }

        public decimal? FineAmount { get; set; }

        public decimal? PaidFineAmount { get; set; }

        public decimal? ServiceChargeArreasAmount { get; set; }

        public decimal? PaidServiceChargeArreasAmount { get; set; }

        //public decimal? CurrentServiceChargeAmount { get; set; }

        //public decimal? PaidCurrentServiceChargeAmount { get; set; }

        //public decimal? CurrentRentalAmount { get; set; }

        //public decimal? PaidCurrentRentalAmount { get; set; }

        public decimal? OverPaymentAmount { get; set; }

        //public decimal? OnTimePaid { get; set; }

        //public decimal? LatePaid { get; set; }

        //public decimal? Payments { get; set; }

        public bool? IsCompleted { get; set; }

        //public bool? IsFilled { get; set; }

        public bool? IsProcessed { get; set; } = false;

        public int? NoOfPayments { get; set; } //
        public int? Status { get; set; }
        public int? OfficeId { get; set; }
        public int? SabhaId { get; set; }
        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        //Mapping 1(property): many (balances)
        public virtual BasicPropertyResource? Property { get; set; }

        //Mapping 1(shop): many (balances)
        public virtual BasicShopResource? Shop { get; set; }

        //---------
        //not mapping fields ----- ignore field
        public virtual PartnerResource? Customer { get; set; }
        //---------


        //-----new
        public bool? HasTransaction { get; set; } //modified 2024/04/09





        //------[Start: fields for Report]-------
        public decimal? LYFine { get; set; }

        public decimal? PaidLYFine { get; set; }
        //----

        public decimal? LYArreas { get; set; }

        public decimal? PaidLYArreas { get; set; }
        //----

        public decimal? TYFine { get; set; }

        public decimal? PaidTYFine { get; set; }
        //----

        public decimal? TYArreas { get; set; }

        public decimal? PaidTYArreas { get; set; }
        //----

        public decimal? TYLYServiceChargeArreas { get; set; }

        public decimal? PaidTYLYServiceChargeArreas { get; set; }
        //----

        public decimal? CurrentServiceChargeAmount { get; set; }

        public decimal? PaidCurrentServiceChargeAmount { get; set; }

        public decimal? CurrentRentalAmount { get; set; }

        public decimal? PaidCurrentRentalAmount { get; set; }

        public decimal? CurrentMonthNewFine { get; set; }

        public decimal? PaidCurrentMonthNewFine { get; set; }

        public int? IsHold { get; set; }
        //------[End: fields for Report]---------
    }
}

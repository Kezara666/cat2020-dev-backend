namespace CAT20.WebApi.Resources.ShopRental
{
    public partial class BasicOpeningBalanceResource
    {

        public int? Id { get; set; }

        public int PropertyId { get; set; } //FK

        public int ShopId { get; set; } //FK

        public int Year { get; set; }

        public int MonthId { get; set; }

        public decimal? LastYearArrearsAmount { get; set; } //Last year arreas amount

        public decimal? ThisYearArrearsAmount { get; set; } //This year arreas amount

        public decimal? LastYearFineAmount { get; set; } //Last year fine amount

        public decimal? ThisYearFineAmount { get; set; } //This year fine amount

        public decimal? OverPaymentAmount { get; set; }

        public decimal? ServiceChargeArreasAmount { get; set; }

        public decimal? CurrentServiceChargeAmount { get; set; } //Monthly service charge

        public decimal? CurrentRentalAmount { get; set; }

        public int? BalanceIdForLastYearArrears { get; set; }

        public int? BalanceIdForCurrentBalance { get; set; }

        public int Status { get; set; }
        public int? OfficeId { get; set; }
        public int? SabhaId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        //mapping--------------------------------------
        public virtual BasicPropertyResource? Property { get; set; } //Mapping 1(Property): many (OpeningBalance)
        public virtual BasicShopResource? Shop { get; set; }  //Mapping 1(Shop): 1 (OpeningBalance)
        //---------------------------------------------

        public bool? IsProcessed { get; set; } = false; //Bill processed?

        //new fields
        public DateTime ApprovedAt { get; set; }
        public int? ApprovedBy { get; set; }
        public string? ApproveComment { get; set; }
        public int ApproveStatus { get; set; } //0-pending | 1-approved | 2-rejected
    }
}

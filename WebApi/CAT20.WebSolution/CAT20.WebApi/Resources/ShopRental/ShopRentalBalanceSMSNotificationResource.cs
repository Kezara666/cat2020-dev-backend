namespace CAT20.WebApi.Resources.ShopRental
{
    public partial class ShopRentalBalanceSMSNotificationResource
    {
        public int? Id { get; set; } //primary key

        public int? PropertyId { get; set; } //Foreign Key field

        public int? ShopId { get; set; } //Foreign Key field

        public int Year { get; set; }

        public int Month { get; set; }

        public int? OfficeId { get; set; }
        public int? SabhaId { get; set; }

        //------[Start: fields for Report]-------
        public bool? IsCompleted { get; set; }

        public decimal? OverPaymentAmount { get; set; }

        public decimal? LYFine { get; set; }

        public decimal? LYArreas { get; set; }

        public decimal? TYFine { get; set; }

        public decimal? TYArreas { get; set; }

        public decimal? TYLYServiceChargeArreas { get; set; }

        public decimal? CurrentServiceChargeAmount { get; set; }

        public decimal? CurrentRentalAmount { get; set; }

        //public bool? IsSMSCreated { get; set; }
        //public bool? IsSMSSend { get; set; }
        //------[End: fields for Report]---------

        //ignore field
        public virtual BasicShopResource? Shop { get; set; }
    }
}

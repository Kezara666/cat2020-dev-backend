namespace CAT20.WebApi.Resources.WaterBilling
{
    public class OpeningBalanceInformationResource
    {

        public int? Id { get; set; }

        public int WaterConnectionId { get; set; }

        //public virtual WaterConnectionResource? WaterConnection { get; set; }

        public virtual ICollection<OBLIApprovalStatusResource>? OBLIApprovalStatus { get; set; }

        public int Month { get; set; }

        public int Year { get; set; } //new

        public decimal? LastYearArrears { get; set; }


        public decimal? MonthlyBalance { get; set; }


        public int? LastMeterReading { get; set; }

        // mandatory fields
        public int? Status { get; set; }
        //public DateTime? CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        public int? isProcessed { get; set; } //new
    }
}

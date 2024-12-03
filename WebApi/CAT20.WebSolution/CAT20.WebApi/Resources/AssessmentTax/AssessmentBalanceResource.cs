namespace CAT20.WebApi.Resources.AssessmentTax
{
    public partial class AssessmentBalanceResource
    {
        public int? Id { get; set; }
        public int? AssessmentId { get; set; }
        public int? Year { get; set; }
        public DateOnly? StartDate { get; set; }


        public decimal? ExcessPayment { get; set; }
        public decimal? LYArrears { get; set; }
        public decimal? LYWarrant { get; set; }
        public decimal? TYArrears { get; set; }
        public decimal? TYWarrant { get; set; }
        public decimal? AnnualAmount { get; set; }
        public decimal? ByExcessDeduction { get; set; }
        public decimal? Paid { get; set; }
        public decimal? OverPayment { get; set; }
        public decimal? DiscountRate { get; set; }
        public decimal? Discount { get; set; }
        public int? CurrentQuarter { get; set; }
        public bool? IsCompleted { get; set; }
        public bool? HasTransaction { get; set; }
        public int? NumberOfPayments { get; set; }
        public int? NumberOfCancels { get; set; }

        public virtual QResource? Q1 { get; set; }
        public virtual QResource? Q2 { get; set; }
        public virtual QResource? Q3 { get; set; }
        public virtual QResource? Q4 { get; set; }



        public virtual AssessmentResource? Assessment { get; set; }


        // mandatory fields
        public int? Status { get; set; }

        //public DateTime? CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}

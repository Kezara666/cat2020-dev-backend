namespace CAT20.WebApi.Resources.AssessmentTax
{
    public partial class AllocationResource
    {


        public int? Id { get; set; }
        public decimal? AllocationAmount { get; set; }
        public DateOnly? ChangedDate { get; set; }
        public string? AllocationDescription { get; set; }
        public int? AssessmentId { get; set; }
        //public virtual AssessmentResource? Assessment { get; set; }

        public int? Status { get; set; }

        // mandatory fields
        //public DateTime? CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        public decimal? NextYearAllocationAmount { get; set; }

        public string? NextYearAllocationDescription { get; set; }


    }
}

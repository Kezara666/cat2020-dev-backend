namespace CAT20.WebApi.Resources.AssessmentTax
{
    public class AssessmentDescriptionLogResource
    {


        public int? Id { get; set; }

        public int DescriptionId { get; set; }
        public string? Comment { get; set; }

        public DescriptionResource? Description { get; set; }

        public int? ActionBy { get; set; }

        public string? ActionNote { get; set; }

        public DateTime? ActivatedDate { get; set; }

        public int? AssessmentId { get; set; }

        //public virtual AssessmentResource? Assessment { get; set; }

        // mandatory fields
        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}

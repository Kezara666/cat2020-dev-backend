namespace CAT20.WebApi.Resources.AssessmentTax
{
    public partial class DescriptionResource
    {


        public int? Id { get; set; }
        public string? DescriptionText { get; set; }
        public int? DescriptionNo { get; set; }
        public int? Status { get; set; }
        public int? SabhaId { get; set; }

        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        public virtual ICollection<AssessmentResource>? Assessments { get; set; }
    }
}

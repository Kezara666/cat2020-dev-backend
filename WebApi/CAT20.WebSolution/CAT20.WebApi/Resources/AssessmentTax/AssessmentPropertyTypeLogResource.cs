namespace CAT20.WebApi.Resources.AssessmentTax
{
    public class AssessmentPropertyTypeLogResource
    {
        public int? Id { get; set; }

        public int PropertyTypeId { get; set; }

        public string? Comment { get; set; }

        public virtual AssessmentPropertyTypeResource? PropertyType { get; set; }

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

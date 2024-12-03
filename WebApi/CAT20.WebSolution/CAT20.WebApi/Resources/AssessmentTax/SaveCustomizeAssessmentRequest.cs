namespace CAT20.WebApi.Resources.AssessmentTax
{
    public class SaveCustomizeAssessmentRequest
    {
       public int AssessmentId { get; set; } 
       //public int ActivationQuarter { get; set; } 

        public decimal ? NewAllocation { get; set; } = -1;

        public int ? NewPropertyTypeId { get; set; } = -1;

        public int? NewDescriptionId { get; set; } = -1;

        public string RequestNote { get; set; } = "";
    }
}

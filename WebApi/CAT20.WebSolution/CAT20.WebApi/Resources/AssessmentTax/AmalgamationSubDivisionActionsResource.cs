using CAT20.Core.Models.AssessmentTax;

namespace CAT20.WebApi.Resources.AssessmentTax
{
    public class AmalgamationSubDivisionActionsResource
    {
        public int? Id { get; set; }
        public int? AmalgamationSubDivisionId { get; set; }
        public int ActionState { get; set; }
        public int ActionBy { get; set; }
        public string? Comment { get; set; }
        public DateTime? ActionDateTime { get; set; }

        //compulsory fields
        public DateTime? SystemActionAt { get; set; }
    }
}

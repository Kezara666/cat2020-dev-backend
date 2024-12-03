using CAT20.Core.Models.Interfaces;

namespace CAT20.WebApi.Resources.AssessmentTax
{
    public class SaveApproveDisapproveResource
    {
        public int AssessmentId { get; set; }
        public string ActionNote { get; set; }

        public int State { get; set;}

    }
}

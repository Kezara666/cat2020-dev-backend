using CAT20.Core.Models.AssessmentTax;
using CAT20.WebApi.Resources.User;
using System.ComponentModel.DataAnnotations;

namespace CAT20.WebApi.Resources.AssessmentTax
{
    public class AssessmentBillAdjustmentResource
    {
        public int Id { get; set; }
        public int AssessmentId { get; set; }
        public int MixOrderId { get; set; }

        public DateTime? RequestDate { get; set; }

        public int? RequestBy { get; set; }

        public string? RequestNote { get; set; }

        public int? DraftApproveRejectWithdraw { get; set; }


        public DateTime? ActionDate { get; set; }

        public int? ActionBy { get; set; }

        public string? ActionNote { get; set; }

        public virtual AssessmentResource? Assessment { get; set; }

        public UserActionByResources? UserRequestBy { get; set; }
        public UserActionByResources? UserActionBy { get; set; }
    }
}

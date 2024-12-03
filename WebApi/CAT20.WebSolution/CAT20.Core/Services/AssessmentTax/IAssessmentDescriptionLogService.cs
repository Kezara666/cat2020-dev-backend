using CAT20.Core.Models.AssessmentTax;

namespace CAT20.Core.Services.AssessmentTax
{
    public interface IAssessmentDescriptionLogService
    {
        Task<bool> Create(AssessmentDescriptionLog obj);
        Task<bool> ApproveDisapproveDescription(AssessmentDescriptionLog obj, bool isApproved);
    }
}

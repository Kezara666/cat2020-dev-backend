using CAT20.Core.Models.AssessmentTax;

namespace CAT20.Core.Services.AssessmentTax
{
    public interface IAssessmentPropertyTypeLogService
    {
        Task<bool> Create(AssessmentPropertyTypeLog obj);
        Task<bool> ApproveDisapprovePropertyType(AssessmentPropertyTypeLog obj, bool isApproved);
    }
}

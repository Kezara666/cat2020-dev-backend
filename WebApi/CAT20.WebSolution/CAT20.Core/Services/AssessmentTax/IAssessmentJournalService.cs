using CAT20.Core.HelperModels;
using CAT20.Core.Models.AssessmentTax;

namespace CAT20.Core.Services.AssessmentTax
{
    public interface IAssessmentJournalService
    {

        Task<(int totalCount, IEnumerable<Assessment> list)> GetForPendingJournalRequest(int? sabhaId, int pageNo);
        Task<(int totalCount, IEnumerable<AssessmentJournal> list)> GetAllJournalRequestForOffice(int? officeId, int pageNo);
        Task<Assessment> GetAssessmentForJournal(int? sabhaId, int? kFormId);
        Task<AssessmentPropertyType> GetRequestedPropterType(int id);

        Task<bool> Create(AssessmentJournal obj,HTokenClaim token);
        Task<bool> ApproveRejectJournal(HApproveRejectJournal obj ,HTokenClaim token);

        Task<(int totalCount, IEnumerable<AssessmentJournal> list)> GetAllJournalForAssessment(int assessmentId, int pageNo);
    }
}

using CAT20.Core.Models.AssessmentTax;

namespace CAT20.Core.Repositories.AssessmentTax
{
    public interface IAssessmentJournalRepository : IRepository<AssessmentJournal>
    {


        Task<(int totalCount, IEnumerable<Assessment> list)> GetForPendingJournalRequest(int? sabhaId, int pageNo);
        Task<(int totalCount, IEnumerable<AssessmentJournal> list)> GetAllJournalRequestForOffice(int? officeId, int pageNo);

        Task<(int totalCount, IEnumerable<AssessmentJournal> list)> GetAllJournalForAssessment(int assessmentId, int pageNo);
    }
}

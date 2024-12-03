using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Models.Enums;

namespace CAT20.Core.Repositories.AssessmentTax
{
    public interface IAssessmentProcessRepository : IRepository<AssessmentProcess>
    {
        Task<(int totalCount, IEnumerable<AssessmentProcess> list)> getAllProcessForSabha(int sabhaId, int pageNo);

        Task<bool> IsCompetedProcess(int SbahaId, int year,AssessmentProcessType processType);
    }
}

using CAT20.Core.Models.AssessmentTax;

namespace CAT20.Core.Repositories.AssessmentTax
{
    public interface IAssessmentPropertyTypeLogRepository : IRepository<AssessmentPropertyTypeLog>
    {

        Task<bool> HasAlreadyLog(int assessmentId);
    }
}

using CAT20.Core.Models.AssessmentTax;

namespace CAT20.Core.Repositories.AssessmentTax
{
    public interface IAssessmentDescriptionLogRepository : IRepository<AssessmentDescriptionLog>
    {

        Task<bool> HasAlreadyLog(int assessmentId);
    }
}

using CAT20.Core.Models.AssessmentTax;

namespace CAT20.Core.Repositories.AssessmentTax
{
    public interface IAssessmentTransactionRepository : IRepository<AssessmentTransaction>
    {

        Task<(int totalCount, IEnumerable<AssessmentTransaction> list)> GetAllTransactionForAssessment(int assessmentId, int pageNo);

        Task<IEnumerable<AssessmentTransaction>> GetLastTransaction(int assessmentId,int numberOfTransection);

        Task<AssessmentTransaction> GetPreviousOpenBalTransaction(int assessmentId);
    }
}

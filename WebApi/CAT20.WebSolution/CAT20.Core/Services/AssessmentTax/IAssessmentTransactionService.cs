using CAT20.Core.Models.AssessmentTax;

namespace CAT20.Core.Services.AssessmentTax
{
    public interface IAssessmentTransactionService
    {

        Task<(int totalCount, IEnumerable<AssessmentTransaction> list)> GetAllTransactionForAssessment(int assessmentId, int pageNo);
    }
}

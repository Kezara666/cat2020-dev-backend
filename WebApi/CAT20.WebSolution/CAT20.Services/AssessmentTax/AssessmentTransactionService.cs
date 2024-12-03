using CAT20.Core;
using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Services.AssessmentTax;

namespace CAT20.Services.AssessmentTax
{
    public class AssessmentTransactionService : IAssessmentTransactionService
    {

        private readonly IAssessmentTaxUnitOfWork _unitOfWork;
        public AssessmentTransactionService(IAssessmentTaxUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<(int totalCount, IEnumerable<AssessmentTransaction> list)> GetAllTransactionForAssessment(int assessmentId, int pageNo)
        {
            try
            {
                return await _unitOfWork.AssessmentTransactions.GetAllTransactionForAssessment(assessmentId, pageNo);
            }
            catch (Exception ex)
            {
                List<AssessmentTransaction> list1 = new List<AssessmentTransaction>();
                return (0, list1);
            }
        }
    }
}
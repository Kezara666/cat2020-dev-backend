using CAT20.Core;
using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Services.AssessmentTax;

namespace CAT20.Services.AssessmentTax
{
    public class AssmVoteTypeService : IAssmtVotePaymentTypeService
    {
        private readonly IAssessmentTaxUnitOfWork _wb_unitOfWork;
        public AssmVoteTypeService(IAssessmentTaxUnitOfWork wb_unitOfWork)
        {
            _wb_unitOfWork = wb_unitOfWork;
        }

        public async Task<IEnumerable<VotePaymentType>> GetAll()
        {
            return await _wb_unitOfWork.AssmtVotePaymentTypes.GetAllAsync();
        }
    }
}

using CAT20.Core.Models.AssessmentTax;

namespace CAT20.Core.Repositories.AssessmentTax
{
    public interface IAssessmentVotePaymentTypeRepository : IRepository<VotePaymentType>
    {
        Task<IEnumerable<VotePaymentType>> GetAll();
    }
}

using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Repositories.AssessmentTax;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Data.Repositories.AssessmentTax
{
    public class AssessmentVotePaymentTypeRepository : Repository<VotePaymentType>, IAssessmentVotePaymentTypeRepository
    {
        public AssessmentVotePaymentTypeRepository(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<VotePaymentType>> GetAll()
        {
            return await assmtDbContext.VotePaymentTypes.Where(c => c.Status == 1).ToListAsync();
        }


        private AssessmentTaxDbContext assmtDbContext
        {
            get { return Context as AssessmentTaxDbContext; }
        }
    }
}

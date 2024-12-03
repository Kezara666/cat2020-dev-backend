using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Repositories.AssessmentTax;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.AssessmentTax
{
    public class AssessmentVoteAssignRepository : Repository<AssessmentVoteAssign>, IAssessmentVoteAssignRepository
    {
        public AssessmentVoteAssignRepository(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<AssessmentVoteAssign>> GetAllForSabha(int sabhaid)
        {
            return await assmtTaxDbContext.AssessmentVoteAssigns.Where(vas=>vas.SabhaId == sabhaid).Include(vas=>vas.VotePaymentType).ToListAsync();   
        }

        private AssessmentTaxDbContext assmtTaxDbContext
        {
            get { return Context as AssessmentTaxDbContext; }
        }
    }
}

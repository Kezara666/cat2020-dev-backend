using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Repositories.AssessmentTax;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Data.Repositories.AssessmentTax
{
    public class AssessmentPropertyTypeLogRepository : Repository<AssessmentPropertyTypeLog>, IAssessmentPropertyTypeLogRepository
    {
        public AssessmentPropertyTypeLogRepository(DbContext context) : base(context)
        {
        }

        public Task<bool> HasAlreadyLog(int assessmentId)
        {
           return assessmentTaxDbContext.AssessmentPropertyTypeLogs.AnyAsync(p => p.AssessmentId == assessmentId);
        }


        private AssessmentTaxDbContext assessmentTaxDbContext
        {
            get { return Context as AssessmentTaxDbContext; }
        }
    }
}

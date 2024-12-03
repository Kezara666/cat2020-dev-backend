using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Repositories.AssessmentTax;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Data.Repositories.AssessmentTax
{
    internal class AssessmentDescriptionLogRepository : Repository<AssessmentDescriptionLog>, IAssessmentDescriptionLogRepository
    {
        public AssessmentDescriptionLogRepository(DbContext context) : base(context)
        {
        }

        public Task<bool> HasAlreadyLog(int assessmentId)
        {
            return assessmentTaxDbContext.AssessmentDescriptionLogs.AnyAsync(p => p.AssessmentId == assessmentId);
        }

        private AssessmentTaxDbContext assessmentTaxDbContext
        {
            get { return Context as AssessmentTaxDbContext; }
        }
    }
}

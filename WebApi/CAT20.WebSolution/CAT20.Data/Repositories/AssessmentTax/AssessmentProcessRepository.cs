using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Repositories.AssessmentTax;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Data.Repositories.AssessmentTax
{
    public class AssessmentProcessRepository : Repository<AssessmentProcess>, IAssessmentProcessRepository
    {
        public AssessmentProcessRepository(DbContext context) : base(context)
        {
        }

        public async Task<(int totalCount, IEnumerable<AssessmentProcess> list)> getAllProcessForSabha(int sabhaId, int pageNo)
        {
            try
            {

                var query = assessmentTaxDbContext.AssessmentProcesses
                    .Where(a => a.ShabaId == sabhaId)
                    .OrderByDescending(a => a.Id); ;

                // Implement your logic to calculate totalCount

                int totalCount = await query.CountAsync();

                var pageSize = 10;
                int skipAmount = (pageNo - 1) * pageSize;

                var assessmentList = await query.Skip(skipAmount).Take(pageSize).ToListAsync();

                return (totalCount: totalCount, list: assessmentList);

            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                // You might want to return an appropriate error response.
                return (totalCount: 0, list: null);
            }
        }

        public async Task<bool> IsCompetedProcess(int sabhaId,int year, AssessmentProcessType processType)
        {
            return await assessmentTaxDbContext.AssessmentProcesses.AnyAsync(p => p.ShabaId == sabhaId &&  p.Year == year && p.ProcessType== processType);
        }

        private AssessmentTaxDbContext assessmentTaxDbContext
        {
            get { return Context as AssessmentTaxDbContext; }
        }
    }
}

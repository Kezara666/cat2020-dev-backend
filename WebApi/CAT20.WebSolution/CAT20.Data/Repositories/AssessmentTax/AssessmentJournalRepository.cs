using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Repositories.AssessmentTax;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Data.Repositories.AssessmentTax
{
    public class AssessmentJournalRepository : Repository<AssessmentJournal>, IAssessmentJournalRepository
    {
        public AssessmentJournalRepository(DbContext context) : base(context)
        {
        }

        public async Task<(int totalCount, IEnumerable<AssessmentJournal> list)> GetAllJournalForAssessment(int assessmentId, int pageNo)
        {
            try
            {

                var query = assessmentTaxDbContext.AssessmentJournals
                    .Where(a => a.AssessmentId == assessmentId)
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

        public async Task<(int totalCount, IEnumerable<Assessment> list)> GetForPendingJournalRequest(int? sabhaId, int pageNo)
        {
            try
            {

                var query = assessmentTaxDbContext.Assessments
               .AsNoTracking()
               .Include(m => m.Street).ThenInclude(st => st.Ward)
               .Include(m => m.AssessmentPropertyType)
               .Include(m => m.Description)
               .Include(m => m.AssessmentTempPartner)
               .Include(m => m.AssessmentTempSubPartner)
               .Include(m => m.AssessmentBalance)
               .Include(a => a.AssessmentBalance.Q1)
               .Include(a => a.AssessmentBalance.Q2)
               .Include(a => a.AssessmentBalance.Q3)
               .Include(a => a.AssessmentBalance.Q4)
               .Include(m => m.Allocation)
               .Include(a => a.AssessmentJournals.OrderByDescending(j => j.Id))
               .Where(a => a.SabhaId == sabhaId && a.HasJournalRequest == true);



                // Implement your logic to calculate totalCount

                int totalCount = await query.CountAsync();

                var pageSize = 10;
                int skipAmount = (pageNo - 1) * pageSize;

                var assessmentList = await query.Skip(skipAmount).Take(pageSize).ToListAsync();

                return (totalCount: totalCount, list: assessmentList);

            }
            catch (Exception ex)
            {

                return (totalCount: 0, list: null);
            }
        }

        public async Task<(int totalCount, IEnumerable<AssessmentJournal> list)> GetAllJournalRequestForOffice(int? officeId, int pageNo)
        {
            try
            {

                var query = assessmentTaxDbContext.AssessmentJournals
                    .AsNoTracking()
                    .Include(j => j.Assessment)
                    .Where(a => a.Assessment!.OfficeId == officeId)
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

        private AssessmentTaxDbContext assessmentTaxDbContext
        {
            get { return Context as AssessmentTaxDbContext; }
        }
    }
}

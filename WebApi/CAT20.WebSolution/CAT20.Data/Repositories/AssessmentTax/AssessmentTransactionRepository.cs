using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Models.Enums;
using CAT20.Core.Repositories.AssessmentTax;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Data.Repositories.AssessmentTax
{
    public class AssessmentTransactionRepository : Repository<AssessmentTransaction>, IAssessmentTransactionRepository
    {
        public AssessmentTransactionRepository(DbContext context) : base(context)
        {
        }

        public async Task<(int totalCount, IEnumerable<AssessmentTransaction> list)> GetAllTransactionForAssessment(int assessmentId, int pageNo)
        {
            try
            {

                var query = assessmentTaxDbContext.AssessmentTransactions
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

        public async Task<IEnumerable<AssessmentTransaction>> GetLastTransaction(int assessmentId, int numberOfTransaction)
        {
            return await assessmentTaxDbContext.AssessmentTransactions.Where(t=>t.AssessmentId == assessmentId).OrderByDescending(t=>t.Id).Take(numberOfTransaction).ToListAsync();
        }

        public async Task<AssessmentTransaction> GetPreviousOpenBalTransaction(int assessmentId)
        {
            if(await assessmentTaxDbContext.AssessmentTransactions.AnyAsync(t => t.AssessmentId == assessmentId && t.Type == AssessmentTransactionsType.SystemAdjustment))
            {
                return await assessmentTaxDbContext.AssessmentTransactions.Where(t => t.AssessmentId == assessmentId && t.Type == AssessmentTransactionsType.SystemAdjustment).OrderByDescending(t => t.Id).FirstOrDefaultAsync();

            }else if(await assessmentTaxDbContext.AssessmentTransactions.AnyAsync(t => t.AssessmentId == assessmentId && t.Type == AssessmentTransactionsType.JournalAdjustment))
            {
                return await assessmentTaxDbContext.AssessmentTransactions.Where(t => t.AssessmentId == assessmentId && t.Type == AssessmentTransactionsType.JournalAdjustment).OrderByDescending(t => t.Id).FirstOrDefaultAsync();
            }
            else
            {
                return await assessmentTaxDbContext.AssessmentTransactions.Where(t => t.AssessmentId == assessmentId && t.Type == AssessmentTransactionsType.Init).OrderByDescending(t => t.Id).FirstOrDefaultAsync();
            }
        }

        private AssessmentTaxDbContext assessmentTaxDbContext
        {
            get { return Context as AssessmentTaxDbContext; }
        }
    }
}

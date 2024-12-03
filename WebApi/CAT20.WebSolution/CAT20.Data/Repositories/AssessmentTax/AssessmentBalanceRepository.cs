using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Models.Enums;
using CAT20.Core.Repositories.AssessmentTax;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Data.Repositories.AssessmentTax
{
    public class AssessmentBalanceRepository : Repository<AssessmentBalance>, IAssessmentBalanceRepository
    {
        public AssessmentBalanceRepository(DbContext context) : base(context)
        {
        }

        public async Task<AssessmentBalance> GetById(int id)
        {
            return await assessmentTaxDbContext.AssessmentBalances
                .Include(m => m.Assessment)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
        }


        public async Task<IEnumerable<AssessmentBalance>> GetAll()
        {
            return
            await assessmentTaxDbContext.AssessmentBalances
            .Include(m => m.Assessment)
                .ToListAsync();
        }

        public async Task<IEnumerable<AssessmentBalance>> GetAllForOffice(int officeId)
        {
            return
                await assessmentTaxDbContext.AssessmentBalances
                .Include(m => m.Assessment)
                .Where(m => m.Assessment.OfficeId == officeId)
                .ToListAsync();
        }

        public async Task<IEnumerable<AssessmentBalance>> GetAllForSabha(int sabhaId)
        {
            return
                await assessmentTaxDbContext.AssessmentBalances
                .Include(m => m.Assessment)
                .Where(m => m.Assessment.SabhaId == sabhaId)
                .ToListAsync();
        }

        public async Task<IEnumerable<AssessmentBalance>> GetAllForAssessmentId(int assesssmentId)
        {
            return
                await assessmentTaxDbContext.AssessmentBalances
                .Include(m => m.Assessment)
                .Where(m => m.AssessmentId == assesssmentId)
                .ToListAsync();
        }

        public async Task<IEnumerable<AssessmentBalance>> GetAllForAssessmentIdAndYear(int assesssmentId, int year)
        {
            return
                await assessmentTaxDbContext.AssessmentBalances
                //.Include(m => m.Assessment)
                //.Where(m => m.AssessmentId == assesssmentId && m.Year==year)
                .ToListAsync();
        }

        public async Task<IEnumerable<AssessmentBalance>> GetAllForAssessmentIdAndYearAndQuarter(int assesssmentId, int year, int quarter)
        {
            return
                await assessmentTaxDbContext.AssessmentBalances
                .Include(m => m.Assessment)
                //.Where(m => m.AssessmentId == assesssmentId && m.Year == year && m.QuarterNumber==quarter)
                .ToListAsync();
        }

        public async Task<IEnumerable<AssessmentBalance>> GetAllForSabhaToProcess(int sabhaId)
        {
            return await assessmentTaxDbContext.AssessmentBalances
               .Include(b => b.Q1)
               .Include(b => b.Q2)
               .Include(b => b.Q3)
               .Include(b => b.Q4)
               .Where(b => b.Assessment!.SabhaId == sabhaId)
               .Where(b=> 
                        b.Assessment!.AssessmentStatus == AssessmentStatus.Active ||
                        b.Assessment!.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        b.Assessment!.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        b.Assessment!.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        b.Assessment!.AssessmentStatus == AssessmentStatus.NextYearSubdivision || 
                        b.Assessment!.AssessmentStatus == AssessmentStatus.NextYearAmalgamation || 
                        b.Assessment!.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive || 
                        b.Assessment!.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive )
               .ToListAsync();
        }

        public Task<IEnumerable<AssessmentBalance>> GetInitProcessForSabha(int sabhaId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AssessmentBalance>> GetJanuaryEndProcessForSabha(int sabhaId)
        {
            return await assessmentTaxDbContext.AssessmentBalances
                .Include(b => b.Q1)
                .Include(b => b.Q2)
                .Include(b => b.Q3)
                .Include(b => b.Q4)
                .Where(b => b.Assessment!.SabhaId == sabhaId && b.IsCompleted == false && b.ExcessPayment != 0)
                .Where(b =>
                        b.Assessment!.AssessmentStatus == AssessmentStatus.Active ||
                        b.Assessment!.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        b.Assessment!.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        b.Assessment!.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        b.Assessment!.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        b.Assessment!.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        b.Assessment!.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        b.Assessment!.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive)
                .ToListAsync();

            //.Include(a => a.Allocation)
            //.Include(a => a.AssessmentPropertyType)
            //.Include(a => a.AssessmentBalance)
            //.Where(a => a.SabhaId == sabhaId && a.AssessmentBalance!.IsCompleted == true)
            //.ToListAsync();
        }

        public async Task<IEnumerable<AssessmentBalance>> GetQ1EndProcessForSabha(int sabhaId)
        {
            return await assessmentTaxDbContext.AssessmentBalances
               .Include(b => b.Q1)
               .Include(b => b.Q2)
               .Include(b => b.Q3)
               .Include(b => b.Q4)
               .Where(b => b.Assessment!.SabhaId == sabhaId)
               .Where(b =>
                        b.Assessment!.AssessmentStatus == AssessmentStatus.Active ||
                        b.Assessment!.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        b.Assessment!.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        b.Assessment!.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        b.Assessment!.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        b.Assessment!.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        b.Assessment!.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        b.Assessment!.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive)
                               .ToListAsync();
        }

        public async Task<IEnumerable<AssessmentBalance>> GetQ2EndProcessForSabha(int sabhaId)
        {
            return await assessmentTaxDbContext.AssessmentBalances
               //.Include(b => b.Q1)
               .Include(b => b.Q2)
               .Include(b => b.Q3)
               .Include(b => b.Q4)
               .Where(b => b.Assessment!.SabhaId == sabhaId)
                               .Where(b =>
                        b.Assessment!.AssessmentStatus == AssessmentStatus.Active ||
                        b.Assessment!.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        b.Assessment!.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        b.Assessment!.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        b.Assessment!.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        b.Assessment!.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        b.Assessment!.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        b.Assessment!.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive)
               .ToListAsync();
        }

        public async Task<IEnumerable<AssessmentBalance>> GetQ3EndProcessForSabha(int sabhaId)
        {
            return await assessmentTaxDbContext.AssessmentBalances
               //.Include(b => b.Q1)
               //.Include(b => b.Q2)
               .Include(b => b.Q3)
               .Include(b => b.Q4)
               .Where(b => b.Assessment!.SabhaId == sabhaId)
                               .Where(b =>
                        b.Assessment!.AssessmentStatus == AssessmentStatus.Active ||
                        b.Assessment!.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        b.Assessment!.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        b.Assessment!.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        b.Assessment!.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        b.Assessment!.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        b.Assessment!.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        b.Assessment!.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive)
               .ToListAsync();
        }

        public async Task<IEnumerable<AssessmentBalance>> GetQ4EndProcessForSabha(int sabhaId)
        {
            return await assessmentTaxDbContext.AssessmentBalances
               //.Include(b => b.Q1)
               //.Include(b => b.Q2)
               //.Include(b => b.Q3)
               .Include(b => b.Q4)
               .Where(b => b.Assessment!.SabhaId == sabhaId)
                               .Where(b =>
                        b.Assessment!.AssessmentStatus == AssessmentStatus.Active ||
                        b.Assessment!.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        b.Assessment!.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        b.Assessment!.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        b.Assessment!.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        b.Assessment!.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        b.Assessment!.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        b.Assessment!.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive)
               .ToListAsync();
        }

        public async Task<AssessmentBalance?> GetByIdToProcessPayment(int assessmentId)
        {
            return await assessmentTaxDbContext.AssessmentBalances
               .Include(m => m.Assessment)
                .Include(b => b.Q1)
                .Include(b => b.Q2)
                .Include(b => b.Q3)
                .Include(b => b.Q4)
               .FirstOrDefaultAsync(b => b.AssessmentId == assessmentId);
        }

        public async Task<IEnumerable<AssessmentBalance>> GetForBackup(int sabhaId)
        {
            return await assessmentTaxDbContext.AssessmentBalances
              .Include(m => m.Assessment)
               .Include(b => b.Q1)
               .Include(b => b.Q2)
               .Include(b => b.Q3)
               .Include(b => b.Q4)
              .Where(b => b.Assessment!.SabhaId == sabhaId)
              .Where(b =>
                        b.Assessment!.AssessmentStatus == AssessmentStatus.Active ||
                        b.Assessment!.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        b.Assessment!.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        b.Assessment!.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        b.Assessment!.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        b.Assessment!.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        b.Assessment!.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        b.Assessment!.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive)
              .ToListAsync();
        }

        public async Task<IEnumerable<AssessmentBalance>> GetForOrderTransaction(List<int?> assessmentIds)
        {
            return await assessmentTaxDbContext.AssessmentBalances.Where(b => assessmentIds.Contains(b.AssessmentId)).ToListAsync();

        }

        public async Task<AssessmentBalance> GetForOrderTransaction(int assessmentIds) => await assessmentTaxDbContext.AssessmentBalances.SingleOrDefaultAsync(b => b.AssessmentId == assessmentIds);



        public async Task<AssessmentBalance> GetForJournal(int assessmentId)
        {
            return await assessmentTaxDbContext.AssessmentBalances
                .Include(b => b.Q1)
                .Include(b => b.Q2)
                .Include(b => b.Q3)
                .Include(b => b.Q4)
                .Where(b => b.AssessmentId == assessmentId && b.IsCompleted == false && b.ExcessPayment != 0)
                .Where(b =>
                        b.Assessment!.AssessmentStatus == AssessmentStatus.Active ||
                        b.Assessment!.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        b.Assessment!.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        b.Assessment!.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        b.Assessment!.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        b.Assessment!.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        b.Assessment!.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        b.Assessment!.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive)
                .FirstOrDefaultAsync();

            //.Include(a => a.Allocation)
            //.Include(a => a.AssessmentPropertyType)
            //.Include(a => a.AssessmentBalance)
            //.Where(a => a.SabhaId == sabhaId && a.AssessmentBalance!.IsCompleted == true)
            //.ToListAsync();
        }

        private AssessmentTaxDbContext assessmentTaxDbContext
        {
            get { return Context as AssessmentTaxDbContext; }
        }

    }
}
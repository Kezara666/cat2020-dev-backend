using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Models.Enums;
using CAT20.Core.Repositories.AssessmentTax;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.AssessmentTax
{
    public class AssessmentQuarterReportRepository : Repository<AssessmentQuarterReport>, IAssessmentQuarterReportRepository
    {

        public AssessmentQuarterReportRepository(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<AssessmentQuarterReport>> GetAllByAssessmentId(int assessmentId)
        {
            return await assessmentTaxDbContext.AssessmentQuarterReport
                .Where(r => r.AssessmentId == assessmentId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Assessment>> GetForInitialReport(int sabhaId)
        {
            
            var x= await assessmentTaxDbContext.Assessments
                .Include(a => a.AssessmentBalance)
                .Include(a => a.AssessmentBalance!.Q1)
                //.Include(a => a.AssessmentBalance!.Q2)
                //.Include(a => a.AssessmentBalance!.Q3)
                //.Include(a => a.AssessmentBalance!.Q4)
                .Include(a =>a.Transactions!.Where(t=>t.Type == AssessmentTransactionsType.Init || t.Type == AssessmentTransactionsType.JournalAdjustment || t.Type== AssessmentTransactionsType.SystemAdjustment))
                .Where(a => a.SabhaId == sabhaId)
                .Where(a =>
                        a.AssessmentStatus == AssessmentStatus.Active ||
                        a.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        a.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        a.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive)
                .ToListAsync();

            return x;
        }


        public async Task<IEnumerable<Assessment>> GetForQ1ReportForSabha(int sabhaId)
        {
            //return await assessmentTaxDbContext.AssessmentBalances
            //    .Include(a => a.Q1)
            //    //.Include(a => a.Q2)
            //    //.Include(a => a.Q3)
            //    //.Include(a => a.Q4)
            //    .Where(a => a.Assessment!.SabhaId == sabhaId)
            //    .Where(a => a.Assessment!.AssessmentStatus == 1)
            //    .ToListAsync();

            return await assessmentTaxDbContext.Assessments
                .Include(a => a.AssessmentBalance)
               .Include(a => a.AssessmentBalance!.Q1)
               //.Include(a => a.AssessmentBalance!.Q2)
               //.Include(a => a.AssessmentBalance!.Q3)
               //.Include(a => a.AssessmentBalance!.Q4)
               .Include(a => a.Transactions!.Where(t => t.Type == AssessmentTransactionsType.WarrantingQ1 || t.Type == AssessmentTransactionsType.Q1End || t.Type == AssessmentTransactionsType.SystemAdjustmentQ1 || t.Type == AssessmentTransactionsType.JournalAdjustmentForce))
               .Include(a=>a.AssessmentQuarterReport!.OrderByDescending(r=>r.Id).Take(1))
               .Where(a => a.SabhaId == sabhaId && a.AssessmentBalance!.Q1!.StartDate != a.AssessmentBalance!.Q1!.EndDate)
               .Where(a =>
                        a.AssessmentStatus == AssessmentStatus.Active ||
                        a.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        a.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        a.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive)
               .ToListAsync();
        }

        public async Task<IEnumerable<Assessment>> GetForQ2ReportForSabha(int sabhaId)
        {
            //return await assessmentTaxDbContext.AssessmentBalances
            //    .Include(a => a.Q1)
            //    //.Include(a => a.Q2)
            //    //.Include(a => a.Q3)
            //    //.Include(a => a.Q4)
            //    .Where(a => a.Assessment!.SabhaId == sabhaId)
            //    .Where(a => a.Assessment!.AssessmentStatus == 1)
            //    .ToListAsync();

            return await assessmentTaxDbContext.Assessments
                .Include(a => a.AssessmentBalance)
               //.Include(a => a.AssessmentBalance!.Q1)
               .Include(a => a.AssessmentBalance!.Q2)
               //.Include(a => a.AssessmentBalance!.Q3)
               //.Include(a => a.AssessmentBalance!.Q4)
               .Include(a => a.Transactions!.Where(t => t.Type == AssessmentTransactionsType.WarrantingQ2 || t.Type == AssessmentTransactionsType.Q2End || t.Type == AssessmentTransactionsType.SystemAdjustmentQ2 || t.Type == AssessmentTransactionsType.JournalAdjustmentForce))
               .Include(a => a.AssessmentQuarterReport!.OrderByDescending(r => r.Id).Take(1))
               .Where(a => a.SabhaId == sabhaId && a.AssessmentBalance!.Q1!.StartDate != a.AssessmentBalance!.Q1!.EndDate)
                .Where(a =>
                        a.AssessmentStatus == AssessmentStatus.Active ||
                        a.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        a.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        a.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive)
               .ToListAsync();
        }

        public async Task<IEnumerable<Assessment>> GetForQ3ReportForSabha(int sabhaId)
        {
                return await assessmentTaxDbContext.Assessments
               .Where(a => a.SabhaId == sabhaId && a.AssessmentBalance!.Q3!.StartDate != a.AssessmentBalance!.Q3!.EndDate)
               .Include(a => a.AssessmentBalance)
               //.Include(a => a.AssessmentBalance!.Q1)
               //.Include(a => a.AssessmentBalance!.Q2)
               .Include(a => a.AssessmentBalance!.Q3)
               //.Include(a => a.AssessmentBalance!.Q4)
               .Include(a => a.Transactions!.Where(t => t.Type == AssessmentTransactionsType.WarrantingQ3 || t.Type == AssessmentTransactionsType.Q3End || t.Type == AssessmentTransactionsType.SystemAdjustmentQ3 || t.Type == AssessmentTransactionsType.JournalAdjustmentForce))
               .Include(a => a.AssessmentQuarterReport!.OrderByDescending(r => r.Id).Take(1))
               .Where(a =>
                        a.AssessmentStatus == AssessmentStatus.Active ||
                        a.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        a.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        a.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive)
               .ToListAsync();
        }

        public async Task<IEnumerable<Assessment>> GetForQ4ReportForSabha(int sabhaId)
        {
           return await assessmentTaxDbContext.Assessments
                .Include(a => a.AssessmentBalance)
               //.Include(a => a.AssessmentBalance!.Q1)
               //.Include(a => a.AssessmentBalance!.Q2)
               //.Include(a => a.AssessmentBalance!.Q3)
               .Include(a => a.AssessmentBalance!.Q4)
               .Include(a => a.Transactions!.Where(t => t.Type == AssessmentTransactionsType.WarrantingQ4 || t.Type == AssessmentTransactionsType.Q4End || t.Type == AssessmentTransactionsType.SystemAdjustmentQ4 || t.Type == AssessmentTransactionsType.JournalAdjustmentForce))
               .Include(a => a.AssessmentQuarterReport!.OrderByDescending(r => r.Id).Take(1))
               //.Where(a => a.SabhaId == sabhaId && a.AssessmentBalance!.Q4!.StartDate != a.AssessmentBalance!.Q4!.EndDate)
               .Where(a =>
                        a.AssessmentStatus == AssessmentStatus.Active ||
                        a.AssessmentStatus == AssessmentStatus.ActiveSubdivide ||
                        a.AssessmentStatus == AssessmentStatus.ActiveAmalgamated ||
                        a.AssessmentStatus == AssessmentStatus.NextYearInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivision ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamation ||
                        a.AssessmentStatus == AssessmentStatus.NextYearSubdivisionInactive ||
                        a.AssessmentStatus == AssessmentStatus.NextYearAmalgamationInactive)
               .ToListAsync();
        }

        public async Task<IEnumerable<AssessmentProcess>> GetAllSbahaFinishedQEND(List<int> includeIds,AssessmentProcessType processType)
        {
            return await assessmentTaxDbContext.AssessmentProcesses
                .Where(p => p.ProcessType == processType && includeIds.Contains(p.ShabaId))
                .ToListAsync();

        }

        public async Task<IEnumerable<AssessmentQuarterReport>> GetForAnnualAmount()
        {
            return await assessmentTaxDbContext.AssessmentQuarterReport
                .Include(r => r.Assessment)
                .ThenInclude(a => a.AssessmentBalance)
                .Where(r=>r.Year==2024)
                .ToListAsync();
        }

        public async Task<bool> HasReportExist(int assessmentId, int year, int qno)
        {
           return await assessmentTaxDbContext.AssessmentQuarterReport.Where(r => r.AssessmentId == assessmentId && r.Year == year && r.QuarterNo == qno).AnyAsync();
        }

        private AssessmentTaxDbContext assessmentTaxDbContext
        {
            get { return Context as AssessmentTaxDbContext; }
        }

    }
}

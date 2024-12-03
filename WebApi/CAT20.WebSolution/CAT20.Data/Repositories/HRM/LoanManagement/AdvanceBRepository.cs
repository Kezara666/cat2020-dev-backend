using CAT20.Core.HelperModels;
using CAT20.Core.Models.HRM.LoanManagement;
using CAT20.Core.Repositories.HRM.LoanManagement;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.HRM.LoanManagement
{
    public class AdvanceBRepository : Repository<AdvanceB>, IAdvanceBRepository
    {
        public AdvanceBRepository(DbContext context) : base(context)
        {

        }

        public async Task<AdvanceB?> GetLoanById(int id)
        {
            return await HRMDbContext.Loans
                //.Include(e => e.LoanOpeningBalances.Where(a => a.StatusId == 1))
                .Include(e => e.AdvanceBSettlements.Where(a => a.StatusId == 1))
                .Include(e => e.AdvanceBAttachments.Where(s => s.StatusId == 1))
                .Where(e => e.Id == id && e.RowStatus == 1)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<AdvanceB>> GetAllLoans()
        {
            return await HRMDbContext.Loans
                //.Include(e => e.LoanOpeningBalances.Where(a => a.StatusId == 1))
                .Include(e => e.AdvanceBSettlements.Where(a => a.StatusId == 1))
                .Include(e => e.AdvanceBAttachments.Where(s => s.StatusId == 1))
                .Where(e => e.RowStatus == 1)
                .OrderByDescending(e => e.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<AdvanceB>> GetAllLoansBySabha(int sabhaid)
        {
            return await HRMDbContext.Loans
                 .Include(e => e.Employee)
                .Include(e => e.Guarantor1)
                .Include(e => e.Guarantor2)
                 .Include(e => e.AdvanceBTypeData)
                .ThenInclude(e => e.AdvanceBTypeLedgerMapping)
                .Include(e => e.AdvanceBSettlements.Where(a => a.StatusId == 1))
                .Include(e => e.AdvanceBAttachments.Where(s => s.StatusId == 1))
                .Where(e => e.RowStatus == 1)
                .Where(e => e.SabhaId == sabhaid && e.RowStatus == 1)
                .OrderByDescending(e => e.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<AdvanceB>> GetAllLoansByOffice(int officeid)
        {
            return await HRMDbContext.Loans
                 .Include(e => e.Employee)
                .Include(e => e.Guarantor1)
                .Include(e => e.Guarantor2)
                .Include(e=> e.AdvanceBTypeData)
                .ThenInclude(e=> e.AdvanceBTypeLedgerMapping)
                .Include(e => e.AdvanceBSettlements.Where(a => a.StatusId == 1))
                .Include(e => e.AdvanceBAttachments.Where(s => s.StatusId == 1))
                .Where(e => e.RowStatus == 1)
                .Where(e => e.OfficeId == officeid && e.RowStatus == 1)
                .OrderByDescending(e => e.Id)
                .ToListAsync();
        }
        public async Task<IEnumerable<AdvanceB>> GetAllLoansByEMPId(int empId)
        {
            return await HRMDbContext.Loans
                //.Include(e => e.LoanOpeningBalances.Where(a => a.StatusId == 1))
                .Include(e => e.AdvanceBSettlements.Where(a => a.StatusId == 1))
                .Include(e => e.AdvanceBAttachments.Where(s => s.StatusId == 1))
                .Where(e => e.RowStatus == 1)
                .Where(e => e.EmployeeId == empId && e.RowStatus == 1)
                .OrderByDescending(e => e.Id)
                .ToListAsync();
        }
        public async Task<IEnumerable<AdvanceB>> GetAllLoansByEMPIdAndLoanTypeId(int empId, int loantypeid)
        {
            return await HRMDbContext.Loans
                //.Include(e => e.LoanOpeningBalances.Where(a => a.StatusId == 1))
                .Include(e => e.AdvanceBSettlements.Where(a => a.StatusId == 1))
                .Include(e => e.AdvanceBAttachments.Where(s => s.StatusId == 1))
                .Where(e => e.RowStatus == 1)
                .Where(e => e.EmployeeId == empId && e.RowStatus == 1)
                .OrderByDescending(e => e.Id)
                .ToListAsync();
        }
        public async Task<IEnumerable<AdvanceB>> GetAllNewLoansBySabhaId(int sabhaid)
        {
            return await HRMDbContext.Loans
                .Include(e => e.Employee)
                .Include(e => e.Guarantor1)
                .Include(e => e.Guarantor2)
                .Include(e => e.AdvanceBSettlements.Where(a => a.StatusId == 1))
                .Include(e => e.AdvanceBAttachments.Where(s => s.StatusId == 1))
                .Where(e => e.RowStatus == 1)
                .Where(e => e.SabhaId == sabhaid && e.IsNew == true && e.RowStatus == 1)
                .OrderByDescending(e => e.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<AdvanceB>> GetAllAdvanceBForSettlementSabhaId(int sabhaid, HTokenClaim token)
        {
            return await HRMDbContext.Loans.Include(e => e.Employee)
               .Include(e => e.AdvanceBSettlements.Where(a => a.StatusId == 1))
               //.Include(e => e.AdvanceBAttachments.Where(s => s.StatusId == 1))
               .Where(e => e.SabhaId == sabhaid && e.IsNew == true && e.RowStatus == 1)
               .ToListAsync();
        }

        private HRMDbContext HRMDbContext
        {
            get { return Context as HRMDbContext; }
        }

    }
}

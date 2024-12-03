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
    public class AdvanceBTypeDataRepository : Repository<AdvanceBTypeData>, IAdvanceBTypeDataRepository
    {
        public AdvanceBTypeDataRepository(DbContext context) : base(context)
        {

        }

        public async Task<AdvanceBTypeData> GetLoanTypeDataById(int id)
        {
            return await HRMDbContext.LoanTypeDatas
                .Include(e => e.AdvanceBTypeLedgerMapping.Where(a => a.StatusId == 1))
                .Where(e => e.Id == id && e.StatusId == 1)
                .FirstOrDefaultAsync();
        }

      
        public async Task<IEnumerable<AdvanceBTypeData>> GetLoanTypeData()
        {
            return await HRMDbContext.LoanTypeDatas
                .Where(e => e.StatusId == 1)
                .ToListAsync();
        }

        public async Task<IEnumerable<AdvanceBTypeData>> GetLoanTypeDataByAccountSystemVersionAndSabhaAsync(int accountSystemVersionId, int sabhaId)
        {
            var x = await HRMDbContext.LoanTypeDatas
                
                .Include(e => e.AdvanceBTypeLedgerMapping)
                .Where(e => e.StatusId == 1
                         && e.AccountSystemVersionId == accountSystemVersionId
                         && e.AdvanceBTypeLedgerMapping.Any(l => l.StatusId == 1 && l.SabhaId == sabhaId))
                .ToListAsync();
            return x;
        }

        private HRMDbContext HRMDbContext
        {
            get { return Context as HRMDbContext; }
        }

        public async Task<IEnumerable<AdvanceBTypeLedgerMapping>> GetAllAdvancedLedgerTypesMappingForSabha(int sabhaId)
        {
            return await HRMDbContext.LoanTypeLedgerMappings
                .Include(e=> e.AdvanceBTypeData)
                .Where(e => e.SabhaId == sabhaId && e.StatusId == 1)
                .ToListAsync();
        }

    }
}

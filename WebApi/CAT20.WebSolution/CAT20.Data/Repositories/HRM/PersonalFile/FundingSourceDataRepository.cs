using CAT20.Core.Models.HRM.PersonalFile;
using CAT20.Core.Repositories.HRM.PersonalFile;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.HRM.PersonalFile
{
    public class FundingSourceDataRepository : Repository<FundingSourceData>, IFundingSourceDataRepository
    {
        public FundingSourceDataRepository(DbContext context) : base(context)
        {

        }

        public async Task<FundingSourceData?> GetFundingSourceDataById(int id)
        {
            return await HRMDbContext.FundingSourceData
                .Where(e => e.Id == id && e.Status == 1)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<FundingSourceData>> GetAllFundingSourceData()
        {
            return await HRMDbContext.FundingSourceData
                .Where(e => e.Status == 1)
                .ToListAsync();
        }

        private HRMDbContext HRMDbContext
        {
            get { return Context as HRMDbContext; }
        }
    }
}
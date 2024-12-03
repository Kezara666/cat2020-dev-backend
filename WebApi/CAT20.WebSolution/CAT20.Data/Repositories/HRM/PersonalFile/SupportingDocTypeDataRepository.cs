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
    public class SupportingDocTypeDataRepository : Repository<SupportingDocTypeData>, ISupportingDocTypeDataRepository
    {
        public SupportingDocTypeDataRepository(DbContext context) : base(context)
        {

        }

        public async Task<SupportingDocTypeData?> GetSupportingDocTypeDataById(int id)
        {
            return await HRMDbContext.SupportingDocTypeData
                .Where(e => e.Id == id && e.Status == 1)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<SupportingDocTypeData>> GetAllSupportingDocTypeData()
        {
            return await HRMDbContext.SupportingDocTypeData
                .Where(e => e.Status == 1)
                .ToListAsync();
        }

        private HRMDbContext HRMDbContext
        {
            get { return Context as HRMDbContext; }
        }
    }
}
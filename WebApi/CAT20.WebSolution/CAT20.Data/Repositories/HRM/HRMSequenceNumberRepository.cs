using CAT20.Core.Models.HRM;
using CAT20.Core.Repositories.HRM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.HRM
{
    public class HRMSequenceNumberRepository : Repository<HRMSequenceNumber>, IHRMSequenceNumberRepository
    {
        public HRMSequenceNumberRepository(DbContext context) : base(context)
        {
        }

        private HRMDbContext HRMDbContext
        {
            get { return Context as HRMDbContext; }
        }

        public Task<HRMSequenceNumber> GetNextSequenceNumberForYearSabhaModuleType(int year, int? sabhaId, int moduleType)
        {
            return HRMDbContext.HRMSequenceNumbers
                .Where(m => m.Year == year && m.SabhaId == sabhaId && m.ModuleType == moduleType)
                .FirstOrDefaultAsync();
        }
    }
   
}

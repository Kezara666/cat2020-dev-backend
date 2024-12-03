using CAT20.Core.Models.WaterBilling;
using CAT20.Core.Repositories.WaterBilling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.WaterBilling
{
    public class WaterConnectionNatureLogRepository : Repository<WaterConnectionNatureLog>, IWaterConnectionNatureLogRepository
    {
        public WaterConnectionNatureLogRepository(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<WaterConnectionNatureLog>> GetAllNaturesByWCId(int wcId)
        {
            return await waterBillingDbContext.WaterConnectionNatureLogs.Where(nlog=>nlog.ConnectionId == wcId).ToListAsync();
        }

        public Task<IEnumerable<WaterConnectionNatureLog>> GetAllNaturesForConnection(string applicationNo, int Id)
        {
            throw new NotImplementedException();
           
        }

        public async Task<WaterConnectionNatureLog> GetFirstNatureByWCId(int wcId)
        {
            return  waterBillingDbContext.WaterConnectionNatureLogs.FirstOrDefault(nlog => nlog.ConnectionId == wcId);
        }

        private WaterBillingDbContext waterBillingDbContext
        {
            get { return Context as WaterBillingDbContext; }
        }
    }
}

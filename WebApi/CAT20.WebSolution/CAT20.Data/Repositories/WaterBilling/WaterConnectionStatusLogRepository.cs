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
    public class WaterConnectionStatusLogRepository : Repository<WaterConnectionStatusLog>, IWaterConnectionStatusLogRepository
    {
        public WaterConnectionStatusLogRepository(DbContext context) : base(context)
        {
        }

        public async Task<WaterConnectionStatusLog> GetFirstStatusByWCId(int wcId)
        {
            return waterBillingDbContext.WaterConnectionStatusLogs.FirstOrDefault(slog => slog.ConnectionId == wcId);
        }

        private WaterBillingDbContext waterBillingDbContext
        {
            get { return Context as WaterBillingDbContext; }
        }
    }
}

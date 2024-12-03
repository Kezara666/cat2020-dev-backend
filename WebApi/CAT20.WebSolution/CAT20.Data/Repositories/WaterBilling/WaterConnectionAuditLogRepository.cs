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
    internal class WaterConnectionAuditLogRepository : Repository<ConnectionAuditLog>, IWaterConnectionAuditLogRepository
    {
        public WaterConnectionAuditLogRepository(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ConnectionAuditLog>> getAllAuditLogsForConnection(int waterConnectionId)
        {
            return await waterBillingDbContext.ConnectionAuditLogs.Where(x => x.WaterConnectionId == waterConnectionId).ToListAsync();
        }

        private WaterBillingDbContext waterBillingDbContext
        {
            get { return Context as WaterBillingDbContext; }
        }
    }
}

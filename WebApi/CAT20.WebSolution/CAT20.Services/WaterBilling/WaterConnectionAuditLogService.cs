using CAT20.Core;
using CAT20.Core.Models.WaterBilling;
using CAT20.Core.Services.WaterBilling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Services.WaterBilling
{
    public class WaterConnectionAuditLogService : IWaterConnectionAuditLogService
    {
        private readonly IWaterBillingUnitOfWork wb_UnitOfWork;

        public WaterConnectionAuditLogService(IWaterBillingUnitOfWork wb_unitOfWork)
        {
            wb_UnitOfWork = wb_unitOfWork;
        }

        public async Task<IEnumerable<ConnectionAuditLog>> getAllAuditLogsForConnection(int waterConnectionId)
        {
            return await wb_UnitOfWork.WaterConnectionAuditLogs.getAllAuditLogsForConnection(waterConnectionId);
        }
    }
}

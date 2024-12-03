using CAT20.Core.Models.WaterBilling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.WaterBilling
{
    public interface IWaterConnectionAuditLogService
    {
        Task<IEnumerable<ConnectionAuditLog>> getAllAuditLogsForConnection(int waterConnectionId);
    }
}

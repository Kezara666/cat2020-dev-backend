using CAT20.Core.Models.Common;
using CAT20.Core.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Repositories.Common
{
    public interface IAuditLogRepository : IRepository<AuditLog>
    {
        Task<IEnumerable<AuditLog>> GetAllForTransaction(string transactionName, int transactionID);
    }
}

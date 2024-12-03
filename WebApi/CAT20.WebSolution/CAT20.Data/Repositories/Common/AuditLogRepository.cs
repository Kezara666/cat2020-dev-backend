using CAT20.Core.Models.Common;
using CAT20.Core.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.Common
{
    public class AuditLogRepository : Repository<AuditLog>, IAuditLogRepository
    {
        public AuditLogRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<AuditLog>> GetAllForTransaction(string transactionName, int transactionID)
        {
            return await controlDbContext.AuditLogs
                .Where(a => a.TransactionName == transactionName && a.TransactionID == transactionID)
                .ToListAsync();
        }

        private ControlDbContext controlDbContext
        {
            get { return Context as ControlDbContext; }
        }

    }
}

using CAT20.Core.Models.Enums;
using CAT20.Core.Models.User;
using CAT20.Core.Repositories.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.User
{
    public class AuditLogUserRepository : Repository<AuditLogUser>, IAuditLogUserRepository
    {
        public AuditLogUserRepository(DbContext context) : base(context)
        {
        }
        private UserActivityDBContext userActivityDbContext
        {
            get { return Context as UserActivityDBContext; }
        }

        public async Task<IEnumerable<AuditLogUser>> GetAllforTransactionAsync(Transaction transaction, int trnansactionID)
        {
            return await userActivityDbContext.AuditLogUsers
                .Where(m => m.Transaction == transaction && m.SourceID == trnansactionID)
                .ToListAsync();
        }
    }
}

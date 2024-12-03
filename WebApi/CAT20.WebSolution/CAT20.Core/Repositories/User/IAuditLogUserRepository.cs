using CAT20.Core.Models.Enums;
using CAT20.Core.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Repositories.User
{
    public interface IAuditLogUserRepository : IRepository<AuditLogUser>
    {
        Task<IEnumerable<AuditLogUser>> GetAllforTransactionAsync(Transaction transaction, int trnansactionID);
    }
}

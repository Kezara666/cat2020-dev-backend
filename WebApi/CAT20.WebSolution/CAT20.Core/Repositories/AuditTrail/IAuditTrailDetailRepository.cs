using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.AuditTrails;

namespace CAT20.Core.Repositories.AuditTrails
{
    public interface IAuditTrailDetailRepository : IRepository<AuditTrailDetail>
    {
        Task<IList<AuditTrailDetail>> GetAllForAsync(AuditTrail auditTrail);
        //Task<IEnumerable<AuditTrail>> GetAllWithAuditTrailAsync();
        //Task<AuditTrail> GetWithAuditTrailByIdAsync(int id);
        //Task<IEnumerable<AuditTrail>> GetAllWithAuditTrailByAuditTrailIdAsync(int Id);
    }
}

using CAT20.Core.Models.AuditTrails;
using CAT20.Core.Repositories.AuditTrails;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.AuditTrails
{
    public class AuditTrailDetailRepository : Repository<AuditTrailDetail>, IAuditTrailDetailRepository
    {
        public AuditTrailDetailRepository(DbContext context) : base(context)
        {
        }

        public async Task<IList<AuditTrailDetail>> GetAllForAsync(AuditTrail auditTrail)
        {
            return await auditTrailDbContext.AuditTrailDetails
                .Where(t => t.AuditTrailID == auditTrail.Id)
                .ToListAsync();
        }


        //public async Task<IEnumerable<AuditTrail>> GetAllWithAuditTrailAsync()
        //{
        //    return await auditTrailDbContext.AuditTrails
        //        .ToListAsync();
        //}

        //public async Task<AuditTrail> GetWithAuditTrailByIdAsync(int id)
        //{
        //    return await auditTrailDbContext.AuditTrails
        //        .Where(m => m.ID == id)
        //        .FirstOrDefaultAsync();
        //}

        //public async Task<IEnumerable<AuditTrail>> GetAllWithAuditTrailByAuditTrailIdAsync(int AuditTrailId)
        //{
        //    return await auditTrailDbContext.AuditTrails
        //        .Where(m => m.ID == AuditTrailId)
        //        .ToListAsync();
        //}

        private AuditTrailDbContext auditTrailDbContext
        {
            get { return Context as AuditTrailDbContext; }
        }
    }
}
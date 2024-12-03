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
    public class AuditTrailRepository : Repository<AuditTrail>, IAuditTrailRepository
    {
        public AuditTrailRepository(DbContext context) : base(context)
        {
        }

        public async Task<AuditTrail> GetSingle(System.Linq.Expressions.Expression<System.Func<AuditTrail, bool>> whereCondition)
        {
            var returnVal = auditTrailDbContext.AuditTrails.Where(whereCondition).FirstOrDefault();

            if (returnVal != null)
            {
                var detailList = await new AuditTrailDetailRepository(auditTrailDbContext).GetAllForAsync(returnVal);
                returnVal.DetailList = detailList;
            }

            return returnVal;
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
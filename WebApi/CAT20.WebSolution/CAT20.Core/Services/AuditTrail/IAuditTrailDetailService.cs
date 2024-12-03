using CAT20.Core.Models.AuditTrails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.AuditTrails
{
    public interface IAuditTrailDetailService
    {
        Task<IList<AuditTrailDetail>> GetAllForAsync(AuditTrail auditTrail);
        //Task<IEnumerable<AuditTrailDetail>> GetAllAuditTrailDetails();
        //Task<AuditTrailDetail> GetAuditTrailDetailById(int id);
        //Task<AuditTrailDetail> CreateAuditTrailDetail(AuditTrailDetail newAuditTrailDetail);
        //Task UpdateAuditTrailDetail(AuditTrailDetail AuditTrailDetailToBeUpdated, AuditTrailDetail AuditTrailDetail);
        //Task DeleteAuditTrailDetail(AuditTrailDetail AuditTrailDetail);
    }
}


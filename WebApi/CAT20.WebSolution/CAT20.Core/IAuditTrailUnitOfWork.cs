using CAT20.Core.Models.AuditTrails;
using CAT20.Core.Repositories.AssessmentTax;
using CAT20.Core.Repositories.AuditTrails;
using System;
using System.Data;
using System.Threading.Tasks;

namespace CAT20.Core
{
    public interface IAuditTrailUnitOfWork : IDisposable, IAsyncDisposable
    {
        IAuditTrailRepository AuditTrails { get; }
        IAuditTrailDetailRepository AuditTrailDetails { get; }
        
        Task<int> CommitAsync();
        IDbTransaction BeginTransaction();
    }
}

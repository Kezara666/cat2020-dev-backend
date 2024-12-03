using CAT20.Core;
using CAT20.Core.Repositories.Common;
using CAT20.Core.Repositories.AuditTrails;
using CAT20.Data.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using CAT20.Data.Repositories.AuditTrails;

namespace CAT20.Data
{
    public class AuditTrailUnitOfWork : IAuditTrailUnitOfWork
    {
        private readonly AuditTrailDbContext _atcontext;

        private AuditTrailRepository _auditTrailRepository;
        private AuditTrailDetailRepository _auditTrailDetailRepository;
        
        public AuditTrailUnitOfWork(AuditTrailDbContext context)
        {
            _atcontext = context;
        }

        public IAuditTrailRepository AuditTrails => _auditTrailRepository = _auditTrailRepository ?? new AuditTrailRepository(_atcontext);
        public IAuditTrailDetailRepository AuditTrailDetails => _auditTrailDetailRepository = _auditTrailDetailRepository ?? new AuditTrailDetailRepository(_atcontext);

        //public async Task<int> CommitAsync()
        //{
        //    return await _context.SaveChangesAsync();
        //}

        ////public void Dispose()
        ////{
        ////    _context.Dispose();
        ////}
        //private bool _disposed = false;
        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!_disposed)
        //    {
        //        if (disposing)
        //        {
        //            _context.Dispose();
        //        }
        //        _disposed = true;
        //    }
        //}

        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}

        //public async ValueTask DisposeAsync()
        //{
        //    Dispose();
        //    await Task.CompletedTask;
        //}
        public async Task<int> CommitAsync()
        {
            return await _atcontext.SaveChangesAsync();
        }

        public IDbTransaction BeginTransaction()
        {
            var transaction = _atcontext.Database.BeginTransaction();
            return transaction.GetDbTransaction();
        }

        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _atcontext.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async ValueTask DisposeAsync()
        {
            Dispose();
            await Task.CompletedTask;
        }
    }
}
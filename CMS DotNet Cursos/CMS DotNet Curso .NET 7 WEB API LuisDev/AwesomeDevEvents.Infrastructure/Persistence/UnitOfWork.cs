using AwesomeDevEvents.Infrastructure.Persistence.Interfaces;
using System;
using System.Threading.Tasks;

namespace AwesomeDevEvents.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitofWork, IDisposable
    {
        private ApplicationDbContext _ctx; // _context 
        private bool _disposed;

        public UnitOfWork(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<bool> CommitAsync()
        {
            return await _ctx.SaveChangesAsync() > 0;
        }

        public Task RollbackAsync()
        {
            return null;
        }

        protected virtual void Dispose(bool disposing)
        {
            //if (!this.disposed)
            //{
            //    if (disposing)
            //    {
            //        context.Dispose();
            //    }
            //}
            if (!_disposed && disposing)
                _ctx.Dispose();
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

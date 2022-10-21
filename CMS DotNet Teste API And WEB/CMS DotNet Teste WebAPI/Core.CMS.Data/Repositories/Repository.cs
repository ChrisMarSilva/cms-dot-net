using Core.CMS.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.CMS.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly BancoDeDadosContext _ctx;
        protected readonly DbSet<T> _db;

        public Repository(BancoDeDadosContext ctx)
        {
            this._ctx = ctx;
            this._db = this._ctx.Set<T>();
        }

        public void Add(T entity)
        {
            this._db.Add(entity);
        }

        public void Update(T entity)
        {
            this._ctx.Update(entity);
        }

        public void Delete(T entity)
        {
            this._db.Remove(entity); 
        }

        public async Task<IEnumerable<T>> GetAsync()
        {
            return await this._db.ToListAsync();
        }

        public async Task<T> GetAsync(object pk)
        {
            return await this._db.FindAsync(pk);
        }
        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    //this._db.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FanSoft.Sotre.Domain.Contracts.Repositories;
using FanSoft.Sotre.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FanSoft.Store.Data.EF.Repositories
{
    public class RepositoryEF<TEntity> : IRepository<TEntity> where TEntity : Entity //where TEntity: class
    {

        private readonly StoreDataContext _ctx;
        protected readonly DbSet<TEntity> _db;

        public RepositoryEF(StoreDataContext ctx)
        {
            _ctx = ctx;
            _db = _ctx.Set<TEntity>();
        }

        public void Add(TEntity entity) => _db.Add(entity);

        public void Update(TEntity entity) => _ctx.Update(entity);  // _db.Update(entity); // _ctx.Entry(entity).State = EntityState.Modified; 

        public void Deletee(TEntity entity) => _db.Remove(entity);

        public async Task<IEnumerable<TEntity>> GetAsync() => await _db.ToListAsync();

        public async Task<TEntity> GetAsync(object pk) => await _db.FindAsync(pk);

    }
}

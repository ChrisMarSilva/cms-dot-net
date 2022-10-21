using CMS.Data.Contexts;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace CMS.Data.Repositories
{
    public class Repository<T> : IRepository<T>  where T : class  
    {
        private readonly BancoDeDadosContext _ctx;
        protected readonly DbSet<T> _db;

        public Repository(BancoDeDadosContext ctx) 
        {
            this._ctx = ctx;
            this._db  = this._ctx.Set<T>();
        }

        public void Add(T entity)
        {
            this._db.Add(entity);
        }

        public void Update(T entity)
        {
            this._ctx.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            //this._ctx.Entry(entity).State = EntityState.Deleted; // somenete se nao esta no Contex
            this._db.Remove(entity); // se caso, jha esta no context, definido no metodo getid
        }

        public async Task<IEnumerable<T>> GetAsync()
        {
            return await this._db.ToListAsync();
        }

        public async Task<T> GetAsync(object pk)
        {
            return await this._db.FindAsync(pk);
        }

    }
}

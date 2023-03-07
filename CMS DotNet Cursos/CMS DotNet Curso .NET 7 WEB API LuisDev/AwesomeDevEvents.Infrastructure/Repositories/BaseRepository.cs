using AwesomeDevEvents.Domain.Models;
using AwesomeDevEvents.Infrastructure.Persistence;
using AwesomeDevEvents.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AwesomeDevEvents.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        private DbSet<T> _entities;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _entities.AsNoTracking().AsEnumerable();
        }

        //public async Task<IEnumerable<T>> Obter(Expression<Func<T, bool>> filter = null)
        //{
        //    var query = _DbSet.AsQueryable();
        //    if (filter != null)
        //        query = query.Where(filter).AsNoTracking();
        //    return await query.ToListAsync();
        //}

        public T Get(Guid Id)
        {
            return _entities.SingleOrDefault(c => c.Id == Id);
        }

        //public async Task<T> ObterPorIdAsync(Guid id)
        //{
        //    return await _DbSet.FindAsync(id);
        //}

        public void Insert(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            _entities.Add(entity);
            _context.SaveChanges();
        }

        //public async Task AddAsync(T entity)
        //{
        //    await _DbSet.AddAsync(entity);
        //    await _AppDbContext.SaveChangesAsync();
        //}

        public void Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            _entities.Update(entity);
            _context.SaveChanges();
        }

        //public async Task Atualizar(T entity)
        //{
        //    _DbSet.Update(entity);
        //    await _AppDbContext.SaveChangesAsync();
        //}

        public void Remove(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            _entities.Remove(entity);
        }

        //public async Task DeletarAsync(T entity)
        //{
        //    _DbSet.Remove(entity);
        //    await _AppDbContext.SaveChangesAsync();
        //}

        public void Delete(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            _entities.Remove(entity);
            _context.SaveChanges();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public bool SaveChangesAsync()
        {
            return _context.SaveChanges() > 0;
        }
    }
}

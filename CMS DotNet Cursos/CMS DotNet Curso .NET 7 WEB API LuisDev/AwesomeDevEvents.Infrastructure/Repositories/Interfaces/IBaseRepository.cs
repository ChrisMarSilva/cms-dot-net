using AwesomeDevEvents.Domain.Models;

namespace AwesomeDevEvents.Infrastructure.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        //  Task<IEnumerable<TEntity>> Obter(Expression<Func<TEntity, bool>> filter = null);
        T Get(Guid Id);
        //  Task<TEntity> ObterPorIdAsync(Guid id);
        void Insert(T entity);
        // Task AddAsync(TEntity entity);
        void Update(T entity);
        // Task Atualizar(TEntity entity);
        void Delete(T entity);
        //Task DeletarAsync(TEntity entity);
        void Remove(T entity);
        bool SaveChanges();
        bool SaveChangesAsync();
    }
}

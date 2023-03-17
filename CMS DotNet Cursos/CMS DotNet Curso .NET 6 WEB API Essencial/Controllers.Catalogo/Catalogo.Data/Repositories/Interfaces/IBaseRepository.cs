using System.Linq.Expressions;

namespace Catalogo.Data.Repositories.Interfaces;

public interface IBaseRepository<T>
{
    IQueryable<T> GetAll(); //Task<IEnumerable<T>> GetAll();
    //------------------------------ TESTADO --------------------------
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);
    Task<T> GetByIdAsync(Expression<Func<T, bool>> expression); // Guid id
    Task<T> GetByIdNoTrackingAsync(Expression<Func<T, bool>> expression); // Guid id
    Task<T> AddAsync(T entity);
    Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
    T Update(T entity);
    bool Remove(T entity);
    bool RemoveRange(IEnumerable<T> entities);
}
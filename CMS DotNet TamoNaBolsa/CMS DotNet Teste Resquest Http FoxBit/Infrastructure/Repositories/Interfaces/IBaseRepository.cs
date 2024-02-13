using System.Linq.Expressions;

namespace CMS_DotNet_Teste_Resquest_Http_FoxBit.Infrastructure.Repositories.Interfaces;

public interface IBaseRepository<T>
{
    IQueryable<T> GetAll(); // Task<IEnumerable<T>>
    Task<IEnumerable<T>> GetByWhereAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
    Task<T> GetByIdAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default); // Guid id
    Task<T> GetByIdNoTrackingAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default); // Guid id
    Task<bool> IsUniqueAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
    //Task<List<T>> LocalizaPaginaAsync(int pagina, int tamanhoPagina);
    Task<int> GetTotalRegistrosAsync();
    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    T Update(T entity);
    bool Remove(T entity);
    bool RemoveRange(IEnumerable<T> entities);    
    Task<bool> RemoveWhereAsync(string tableName, string where);
    Task<bool> RemoveAllAsync(string tableName);
}
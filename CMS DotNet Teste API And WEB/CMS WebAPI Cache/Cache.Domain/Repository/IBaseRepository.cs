using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Cache.Domain.Repository;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<IBaseRepositoryTransaction> BeginTransactionAsync();
    Task SaveChangesAsync();
    void RemoveTrack<T>(T entity) where T : class;
    void StartTrack<T>(T entity) where T : class;

    void ChangeTable(string table);

    TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool asSplitQuery = false, bool includeCondition = true, bool disableTracking = true, bool ignoreQueryFilters = false);
    TResult GetFirstOrDefault<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool asSplitQuery = false, bool includeCondition = true, bool disableTracking = true, bool ignoreQueryFilters = false);
    Task<TResult> GetFirstOrDefaultAsync<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool asSplitQuery = false, bool includeCondition = true, bool disableTracking = true, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default);
    Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool asSplitQuery = false, bool includeCondition = true, bool disableTracking = true, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default);

    TEntity GetSingleOrDefault(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool asSplitQuery = false, bool includeCondition = true, bool disableTracking = true, bool ignoreQueryFilters = false);
    TResult GetSingleOrDefault<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool asSplitQuery = false, bool includeCondition = true, bool disableTracking = true, bool ignoreQueryFilters = false);
    Task<TResult> GetSingleOrDefaultAsync<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool asSplitQuery = false, bool includeCondition = true, bool disableTracking = true, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default);
    Task<TEntity> GetSingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool asSplitQuery = false, bool includeCondition = true, bool disableTracking = true, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default);

    IQueryable<TEntity> FromSql(string sql, params object[] parameters);

    TEntity Find(params object[] keyValues);
    ValueTask<TEntity> FindAsync(params object[] keyValues);
    ValueTask<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken);

    IQueryable<TEntity> GetAll();
    IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool asSplitQuery = false, bool includeCondition = true, bool disableTracking = true, bool ignoreQueryFilters = false);
    IQueryable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool asSplitQuery = false, bool includeCondition = true, bool disableTracking = true, bool ignoreQueryFilters = false);
    Task<IList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool asSplitQuery = false, bool includeCondition = true, bool disableTracking = true, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default);
    Task<IList<TResult>> GetAllAsync<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool asSplitQuery = false, bool includeCondition = true, bool disableTracking = true, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default);

    IPagedList<TEntity> GetPagedList(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool asSplitQuery = false, int pageIndex = 0, int pageSize = 20, bool includeCondition = true, bool disableTracking = true, bool ignoreQueryFilters = false, bool executeCount = true);
    Task<IPagedList<TEntity>> GetPagedListAsync(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool asSplitQuery = false, int pageIndex = 0, int pageSize = 20, bool includeCondition = true, bool disableTracking = true, bool ignoreQueryFilters = false, bool executeCount = true, CancellationToken cancellationToken = default);
    IPagedList<TResult> GetPagedList<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool asSplitQuery = false, int pageIndex = 0, int pageSize = 20, bool includeCondition = true, bool disableTracking = true, bool ignoreQueryFilters = false, bool executeCount = true) where TResult : class;
    Task<IPagedList<TResult>> GetPagedListAsync<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool asSplitQuery = false, int pageIndex = 0, int pageSize = 20, bool includeCondition = true, bool disableTracking = true, bool ignoreQueryFilters = false, bool executeCount = true, CancellationToken cancellationToken = default) where TResult : class;

    int Count(Expression<Func<TEntity, bool>> predicate = null);
    Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null, CancellationToken cancellationToken = default);
    long LongCount(Expression<Func<TEntity, bool>> predicate = null);
    Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate = null, CancellationToken cancellationToken = default);

    T Max<T>(Expression<Func<TEntity, T>> selector, Expression<Func<TEntity, bool>> predicate = null);
    Task<T> MaxAsync<T>(Expression<Func<TEntity, T>> selector, Expression<Func<TEntity, bool>> predicate = null, CancellationToken cancellationToken = default);

    T Min<T>(Expression<Func<TEntity, T>> selector, Expression<Func<TEntity, bool>> predicate = null);
    Task<T> MinAsync<T>(Expression<Func<TEntity, T>> selector, Expression<Func<TEntity, bool>> predicate = null, CancellationToken cancellationToken = default);

    decimal Average(Expression<Func<TEntity, decimal>> selector, Expression<Func<TEntity, bool>> predicate = null);
    Task<decimal> AverageAsync(Expression<Func<TEntity, decimal>> selector, Expression<Func<TEntity, bool>> predicate = null, CancellationToken cancellationToken = default);

    decimal Sum(Expression<Func<TEntity, decimal>> selector, Expression<Func<TEntity, bool>> predicate = null);
    Task<decimal> SumAsync(Expression<Func<TEntity, decimal>> selector, Expression<Func<TEntity, bool>> predicate = null, CancellationToken cancellationToken = default);

    bool Exists(Expression<Func<TEntity, bool>> selector = null);
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> selector = null, CancellationToken cancellationToken = default);

    TEntity Insert(TEntity entity);
    void Insert(params TEntity[] entities);
    void Insert(IEnumerable<TEntity> entities);
    ValueTask<EntityEntry<TEntity>> InsertAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task InsertAsync(params TEntity[] entities);
    Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    void Update(TEntity entity);
    void Update(params TEntity[] entities);
    void Update(IEnumerable<TEntity> entities);

    void Delete(object id);
    void Delete(TEntity entity);
    void Delete(params TEntity[] entities);
    void Delete(IEnumerable<TEntity> entities);

    void ChangeEntityState(TEntity entity, EntityState state);
}
using Cache.Domain.Repository;
using Cache.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;
using System.Reflection;

namespace Cache.Infra.Data.Repository;

public class BaseRepository<TEntity> : IBaseRepositoryTransaction, IBaseRepository<TEntity> where TEntity : class
{
    protected readonly AppDbContext DataContext;
    protected readonly IUnitOfWork UnitOfWork;
    protected IDbContextTransaction? Transaction;
    private readonly DbSet<TEntity> _dbSet;
    private bool _disposedValue;

    public BaseRepository(AppDbContext dataContext, IUnitOfWork unitOfWork)
    {
        DataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _dbSet = DataContext.Set<TEntity>();
    }

    public async Task<IBaseRepositoryTransaction> BeginTransactionAsync()
    {
        Transaction = await UnitOfWork.BeginTransactionAsync();
        return this;
    }

    public async Task CommitAsync()
    {
        ArgumentNullException.ThrowIfNull(Transaction, "_transaction != null");

        await Transaction.CommitAsync();
    }

    public async Task RollbackAsync()
    {
        ArgumentNullException.ThrowIfNull(Transaction, "_transaction != null");

        await Transaction.RollbackAsync();
    }

    public async Task SaveChangesAsync()
    {
        await UnitOfWork.SaveChangesAsync();
    }

    public void RemoveTrack<T>(T entity) where T : class
    {
        DataContext.Set<T>().Entry(entity).State = EntityState.Detached;
    }

    public void StartTrack<T>(T entity) where T : class
    {
        DataContext.Set<T>().Attach(entity);
    }

    public virtual void ChangeTable(string table)
    {
        if (DataContext.Model.FindEntityType(typeof(TEntity)) is IConventionEntityType relational)
            relational.SetTableName(table);
    }

    //If your result set returns 0 records:
    //SingleOrDefault returns the default value for the type(e.g. default for int is 0)
    //FirstOrDefault returns the default value for the type

    //If you result set returns 1 record:
    //SingleOrDefault returns that record
    //FirstOrDefault returns that record

    //If your result set returns many records:
    //SingleOrDefault throws an exception
    //FirstOrDefault returns the first record

    public virtual TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool asSplitQuery = false, bool includeCondition = true, bool disableTracking = true, bool ignoreQueryFilters = false)
    {
        IQueryable<TEntity> query = _dbSet;

        if (disableTracking)
            query = query.AsNoTracking();

        if (include is not null && includeCondition)
            query = asSplitQuery ? include(query).AsSplitQuery() : include(query);

        if (predicate is not null)
            query = query.Where(predicate);

        if (ignoreQueryFilters)
            query = query.IgnoreQueryFilters();

        return orderBy is not null
            ? orderBy(query).FirstOrDefault()
            : query.FirstOrDefault();
    }

    public virtual async Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool asSplitQuery = false, bool includeCondition = true, bool disableTracking = true, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = _dbSet;

        if (disableTracking)
            query = query.AsNoTracking();

        if (include is not null && includeCondition)
            query = asSplitQuery ? include(query).AsSplitQuery() : include(query);

        if (predicate is not null)
            query = query.Where(predicate);

        if (ignoreQueryFilters)
            query = query.IgnoreQueryFilters();

        if (orderBy is not null)
            return await orderBy(query).FirstOrDefaultAsync(cancellationToken: cancellationToken);

        return await query.FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public virtual TResult GetFirstOrDefault<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool asSplitQuery = false, bool includeCondition = true, bool disableTracking = true, bool ignoreQueryFilters = false)
    {
        IQueryable<TEntity> query = _dbSet;

        if (disableTracking)
            query = query.AsNoTracking();

        if (include is not null && includeCondition)
            query = asSplitQuery ? include(query).AsSplitQuery() : include(query);

        if (predicate is not null)
            query = query.Where(predicate);

        if (ignoreQueryFilters)
            query = query.IgnoreQueryFilters();

        return orderBy is not null
            ? orderBy(query).Select(selector).FirstOrDefault()
            : query.Select(selector).FirstOrDefault();
    }

    public virtual async Task<TResult> GetFirstOrDefaultAsync<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool asSplitQuery = false, bool includeCondition = true, bool disableTracking = true, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = _dbSet;

        if (disableTracking)
            query = query.AsNoTracking();

        if (include is not null && includeCondition)
            query = asSplitQuery ? include(query).AsSplitQuery() : include(query);

        if (predicate is not null)
            query = query.Where(predicate);

        if (ignoreQueryFilters)
            query = query.IgnoreQueryFilters();

        if (orderBy is not null)
            return await orderBy(query).Select(selector).FirstOrDefaultAsync(cancellationToken: cancellationToken);

        return await query.Select(selector).FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public TEntity GetSingleOrDefault(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool asSplitQuery = false, bool includeCondition = true, bool disableTracking = true, bool ignoreQueryFilters = false)
    {
        IQueryable<TEntity> query = _dbSet;

        if (disableTracking)
            query = query.AsNoTracking();

        if (include is not null && includeCondition)
            query = asSplitQuery ? include(query).AsSplitQuery() : include(query);

        if (predicate is not null)
            query = query.Where(predicate);

        if (ignoreQueryFilters)
            query = query.IgnoreQueryFilters();

        return orderBy is not null
            ? orderBy(query).SingleOrDefault()
            : query.SingleOrDefault();
    }

    public TResult GetSingleOrDefault<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool asSplitQuery = false, bool includeCondition = true, bool disableTracking = true, bool ignoreQueryFilters = false)
    {
        IQueryable<TEntity> query = _dbSet;

        if (disableTracking)
            query = query.AsNoTracking();

        if (include is not null && includeCondition)
            query = asSplitQuery ? include(query).AsSplitQuery() : include(query);

        if (predicate is not null)
            query = query.Where(predicate);

        if (ignoreQueryFilters)
            query = query.IgnoreQueryFilters();

        return orderBy is not null
            ? orderBy(query).Select(selector).SingleOrDefault()
            : query.Select(selector).SingleOrDefault();
    }

    public async Task<TResult> GetSingleOrDefaultAsync<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool asSplitQuery = false, bool includeCondition = true, bool disableTracking = true, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = _dbSet;

        if (disableTracking)
            query = query.AsNoTracking();

        if (include is not null && includeCondition)
            query = asSplitQuery ? include(query).AsSplitQuery() : include(query);

        if (predicate is not null)
            query = query.Where(predicate);

        if (ignoreQueryFilters)
            query = query.IgnoreQueryFilters();

        if (orderBy is not null)
            return await orderBy(query).Select(selector).SingleOrDefaultAsync(cancellationToken: cancellationToken);

        return await query.Select(selector).SingleOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public async Task<TEntity> GetSingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool asSplitQuery = false, bool includeCondition = true, bool disableTracking = true, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = _dbSet;

        if (disableTracking)
            query = query.AsNoTracking();

        if (include is not null && includeCondition)
            query = asSplitQuery ? include(query).AsSplitQuery() : include(query);

        if (predicate is not null)
            query = query.Where(predicate);

        if (ignoreQueryFilters)
            query = query.IgnoreQueryFilters();

        if (orderBy is not null)
            return await orderBy(query).SingleOrDefaultAsync(cancellationToken: cancellationToken);

        return await query.SingleOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public virtual IQueryable<TEntity> FromSql(string sql, params object[] parameters) =>
        _dbSet.FromSqlRaw(sql, parameters);

    public virtual TEntity Find(params object[] keyValues) =>
        _dbSet.Find(keyValues);

    public virtual ValueTask<TEntity> FindAsync(params object[] keyValues) =>
        _dbSet.FindAsync(keyValues);

    public virtual ValueTask<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken) =>
        _dbSet.FindAsync(keyValues, cancellationToken);

    public IQueryable<TEntity> GetAll() =>
        _dbSet;

    public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool asSplitQuery = false, bool includeCondition = true, bool disableTracking = true, bool ignoreQueryFilters = false)
    {
        IQueryable<TEntity> query = _dbSet;

        if (disableTracking)
            query = query.AsNoTracking();

        if (include is not null && includeCondition)
            query = asSplitQuery ? include(query).AsSplitQuery() : include(query);

        if (predicate is not null)
            query = query.Where(predicate);

        if (ignoreQueryFilters)
            query = query.IgnoreQueryFilters();

        return orderBy is not null ? orderBy(query) : query;
    }

    public IQueryable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool asSplitQuery = false, bool includeCondition = true, bool disableTracking = true, bool ignoreQueryFilters = false)
    {
        IQueryable<TEntity> query = _dbSet;

        if (disableTracking)
            query = query.AsNoTracking();

        if (include is not null && includeCondition)
            query = asSplitQuery ? include(query).AsSplitQuery() : include(query);

        if (predicate is not null)
            query = query.Where(predicate);

        if (ignoreQueryFilters)
            query = query.IgnoreQueryFilters();

        return orderBy is not null ? orderBy(query).Select(selector) : query.Select(selector);
    }

    public async Task<IList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await _dbSet.ToListAsync(cancellationToken: cancellationToken);

    public async Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool asSplitQuery = false, bool includeCondition = true, bool disableTracking = true, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = _dbSet;

        if (disableTracking)
            query = query.AsNoTracking();

        if (include is not null && includeCondition)
            query = asSplitQuery ? include(query).AsSplitQuery() : include(query);

        if (predicate is not null)
            query = query.Where(predicate);

        if (ignoreQueryFilters)
            query = query.IgnoreQueryFilters();

        if (orderBy is not null)
            return await orderBy(query).ToListAsync(cancellationToken: cancellationToken);

        return await query.ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<IList<TResult>> GetAllAsync<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool asSplitQuery = false, bool includeCondition = true, bool disableTracking = true, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = _dbSet;

        if (disableTracking)
            query = query.AsNoTracking();

        if (include is not null && includeCondition)
            query = asSplitQuery ? include(query).AsSplitQuery() : include(query);

        if (predicate is not null)
            query = query.Where(predicate);

        if (ignoreQueryFilters)
            query = query.IgnoreQueryFilters();

        if (orderBy is not null)
            return await orderBy(query).Select(selector).ToListAsync(cancellationToken: cancellationToken);

        return await query.Select(selector).ToListAsync(cancellationToken: cancellationToken);
    }

    public virtual IPagedList<TEntity> GetPagedList(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool asSplitQuery = false, int pageIndex = 0, int pageSize = 20, bool includeCondition = true, bool disableTracking = true, bool ignoreQueryFilters = false, bool executeCount = true)
    {
        IQueryable<TEntity> query = _dbSet;

        if (disableTracking)
            query = query.AsNoTracking();

        if (include is not null && includeCondition)
            query = asSplitQuery ? include(query).AsSplitQuery() : include(query);

        if (predicate is not null)
            query = query.Where(predicate);

        if (ignoreQueryFilters)
            query = query.IgnoreQueryFilters();

        return orderBy is not null
            ? orderBy(query).ToPagedList(pageIndex, pageSize, executeCount: executeCount)
            : query.ToPagedList(pageIndex, pageSize, executeCount: executeCount);
    }

    public virtual Task<IPagedList<TEntity>> GetPagedListAsync(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool asSplitQuery = false, int pageIndex = 0, int pageSize = 20, bool includeCondition = true, bool disableTracking = true, bool ignoreQueryFilters = false, bool executeCount = true, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = _dbSet;

        if (disableTracking)
            query = query.AsNoTracking();

        if (include is not null && includeCondition)
            query = asSplitQuery ? include(query).AsSplitQuery() : include(query);

        if (predicate is not null)
            query = query.Where(predicate);

        if (ignoreQueryFilters)
            query = query.IgnoreQueryFilters();

        return orderBy is not null
            ? orderBy(query).ToPagedListAsync(pageIndex, pageSize, 0, executeCount: executeCount, cancellationToken)
            : query.ToPagedListAsync(pageIndex, pageSize, 0, executeCount: executeCount, cancellationToken);
    }

    public virtual IPagedList<TResult> GetPagedList<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool asSplitQuery = false, int pageIndex = 0, int pageSize = 20, bool includeCondition = true, bool disableTracking = true, bool ignoreQueryFilters = false, bool executeCount = true) where TResult : class
    {
        IQueryable<TEntity> query = _dbSet;

        if (disableTracking)
            query = query.AsNoTracking();

        if (include is not null && includeCondition)
            query = asSplitQuery ? include(query).AsSplitQuery() : include(query);

        if (predicate is not null)
            query = query.Where(predicate);

        if (ignoreQueryFilters)
            query = query.IgnoreQueryFilters();

        return orderBy is not null
            ? orderBy(query).Select(selector).ToPagedList(pageIndex, pageSize, executeCount: executeCount)
            : query.Select(selector).ToPagedList(pageIndex, pageSize, executeCount: executeCount);
    }

    public virtual Task<IPagedList<TResult>> GetPagedListAsync<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool asSplitQuery = false, int pageIndex = 0, int pageSize = 20, bool includeCondition = true, bool disableTracking = true, bool ignoreQueryFilters = false, bool executeCount = true, CancellationToken cancellationToken = default) where TResult : class
    {
        IQueryable<TEntity> query = _dbSet;

        if (disableTracking)
            query = query.AsNoTracking();

        if (include is not null && includeCondition)
            query = asSplitQuery ? include(query).AsSplitQuery() : include(query);

        if (predicate is not null)
            query = query.Where(predicate);

        if (ignoreQueryFilters)
            query = query.IgnoreQueryFilters();

        return orderBy is not null
            ? orderBy(query).Select(selector).ToPagedListAsync(pageIndex, pageSize, 0, executeCount: executeCount, cancellationToken)
            : query.Select(selector).ToPagedListAsync(pageIndex, pageSize, 0, executeCount: executeCount, cancellationToken);
    }

    public virtual int Count(Expression<Func<TEntity, bool>> predicate = null) =>
        predicate is null
            ? _dbSet.Count()
            : _dbSet.Count(predicate);

    public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null, CancellationToken cancellationToken = default)
    {
        if (predicate is null)
            return await _dbSet.CountAsync(cancellationToken: cancellationToken);

        return await _dbSet.CountAsync(predicate, cancellationToken: cancellationToken);
    }

    public virtual long LongCount(Expression<Func<TEntity, bool>> predicate = null) =>
        predicate is null
            ? _dbSet.LongCount()
            : _dbSet.LongCount(predicate);

    public virtual async Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate = null, CancellationToken cancellationToken = default)
    {
        if (predicate is null)
            return await _dbSet.LongCountAsync(cancellationToken: cancellationToken);

        return await _dbSet.LongCountAsync(predicate, cancellationToken: cancellationToken);
    }

    public virtual T Max<T>(Expression<Func<TEntity, T>> selector, Expression<Func<TEntity, bool>> predicate = null) =>
        predicate is null
            ? _dbSet.Max(selector)
            : _dbSet.Where(predicate).Max(selector);

    public virtual async Task<T> MaxAsync<T>(Expression<Func<TEntity, T>> selector, Expression<Func<TEntity, bool>> predicate = null, CancellationToken cancellationToken = default)
    {
        if (predicate is null)
            return await _dbSet.MaxAsync(selector, cancellationToken: cancellationToken);

        return await _dbSet.Where(predicate).MaxAsync(selector, cancellationToken: cancellationToken);
    }

    public virtual T Min<T>(Expression<Func<TEntity, T>> selector, Expression<Func<TEntity, bool>> predicate = null) =>
        predicate is null
            ? _dbSet.Min(selector)
            : _dbSet.Where(predicate).Min(selector);

    public virtual async Task<T> MinAsync<T>(Expression<Func<TEntity, T>> selector, Expression<Func<TEntity, bool>> predicate = null, CancellationToken cancellationToken = default)
    {
        if (predicate is null)
            return await _dbSet.MinAsync(selector, cancellationToken: cancellationToken);

        return await _dbSet.Where(predicate).MinAsync(selector, cancellationToken: cancellationToken);
    }

    public virtual decimal Average(Expression<Func<TEntity, decimal>> selector, Expression<Func<TEntity, bool>> predicate = null) =>
        predicate is null
            ? _dbSet.Average(selector)
            : _dbSet.Where(predicate).Average(selector);

    public virtual async Task<decimal> AverageAsync(Expression<Func<TEntity, decimal>> selector, Expression<Func<TEntity, bool>> predicate = null, CancellationToken cancellationToken = default)
    {
        if (predicate is null)
            return await _dbSet.AverageAsync(selector, cancellationToken: cancellationToken);

        return await _dbSet.Where(predicate).AverageAsync(selector, cancellationToken: cancellationToken);
    }

    public virtual decimal Sum(Expression<Func<TEntity, decimal>> selector, Expression<Func<TEntity, bool>> predicate = null) =>
        predicate is null
            ? _dbSet.Sum(selector)
            : _dbSet.Where(predicate).Sum(selector);

    public virtual async Task<decimal> SumAsync(Expression<Func<TEntity, decimal>> selector, Expression<Func<TEntity, bool>> predicate = null, CancellationToken cancellationToken = default)
    {
        if (predicate is null)
            return await _dbSet.SumAsync(selector, cancellationToken: cancellationToken);

        return await _dbSet.Where(predicate).SumAsync(selector, cancellationToken: cancellationToken);
    }

    public bool Exists(Expression<Func<TEntity, bool>> selector = null) =>
        selector is null
            ? _dbSet.Any()
            : _dbSet.Any(selector);

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> selector = null, CancellationToken cancellationToken = default)
    {
        if (selector is null)
            return await _dbSet.AnyAsync(cancellationToken: cancellationToken);

        return await _dbSet.AnyAsync(selector, cancellationToken: cancellationToken);
    }

    public virtual TEntity Insert(TEntity entity) =>
        _dbSet.Add(entity).Entity;

    public virtual void Insert(params TEntity[] entities) =>
        _dbSet.AddRange(entities);

    public virtual void Insert(IEnumerable<TEntity> entities) =>
        _dbSet.AddRange(entities);

    public virtual ValueTask<EntityEntry<TEntity>> InsertAsync(TEntity entity, CancellationToken cancellationToken = default) =>
        _dbSet.AddAsync(entity, cancellationToken);

    public virtual Task InsertAsync(params TEntity[] entities) =>
        _dbSet.AddRangeAsync(entities);

    public virtual Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) =>
        _dbSet.AddRangeAsync(entities, cancellationToken);

    public virtual void Update(TEntity entity) =>
        _dbSet.Update(entity);

    public virtual void Update(params TEntity[] entities) =>
        _dbSet.UpdateRange(entities);

    public virtual void Update(IEnumerable<TEntity> entities) =>
        _dbSet.UpdateRange(entities);

    public virtual void Delete(TEntity entity) =>
        _dbSet.Remove(entity);

    public virtual void Delete(object id)
    {
        var typeInfo = typeof(TEntity).GetTypeInfo();
        var key = DataContext.Model.FindEntityType(typeInfo)?.FindPrimaryKey()?.Properties.FirstOrDefault();

        PropertyInfo property = null;
        if (key is not null)
            property = typeInfo.GetProperty(key.Name);

        if (property is not null)
        {
            var entity = Activator.CreateInstance<TEntity>();
            property.SetValue(entity, id);
            DataContext.Entry(entity).State = EntityState.Deleted;
        }
        else
        {
            var entity = _dbSet.Find(id);
            if (entity is not null)
                Delete(entity);
        }
    }

    public virtual void Delete(params TEntity[] entities) =>
        _dbSet.RemoveRange(entities);

    public virtual void Delete(IEnumerable<TEntity> entities) =>
        _dbSet.RemoveRange(entities);

    public void ChangeEntityState(TEntity entity, EntityState state) =>
        DataContext.Entry(entity).State = state;

    protected virtual void Dispose(bool disposing)
    {
        if (_disposedValue) return;

        if (disposing)
        {
            Transaction?.Dispose();
        }

        _disposedValue = true;
    }

    private async Task DisposeAsync(bool disposing)
    {
        if (_disposedValue)
            return;

        if (disposing)
        {
            if (Transaction is not null)
                await Transaction.DisposeAsync();
        }

        _disposedValue = true;
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(true);
        GC.SuppressFinalize(this);
    }
}

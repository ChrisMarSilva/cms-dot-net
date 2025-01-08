using Cache.Api.Database.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace Cache.Api.Repositories;

public interface IRepository<T> where T : class
{
    IQueryable<T> GetAll();
    IQueryable<T> ListarPor(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties);
    IQueryable<T> ListarEOrdenadosPor<TKey>(Expression<Func<T, bool>> where, Expression<Func<T, TKey>> ordem, bool ascendente = true, params Expression<Func<T, object>>[] includeProperties);
    IQueryable<T> Listar(params Expression<Func<T, object>>[] includeProperties);
    IQueryable<T> ListarOrdenadosPor<TKey>(Expression<Func<T, TKey>> ordem, bool ascendente = true, params Expression<Func<T, object>>[] includeProperties);
    //T ObterPor(Func<T, bool> where, params Expression<Func<T, object>>[] includeProperties);
    //bool Existe(Func<T, bool> where);
    //T ObterPorId(TId id, params Expression<Func<T, object>>[] includeProperties);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> LocalizaPaginaAsync(int pagina, int tamanhoPagina, CancellationToken cancellationToken = default);
    Task<T?> GetByIdAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
    Task<T?> GetByIdNoTrackingAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> GetByWhereAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    Task<bool> IsUniqueAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
    Task<int> GetTotalRegistrosAsync(CancellationToken cancellationToken = default);
    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    T Update(T entity, CancellationToken cancellationToken = default);
    void Delete(T entity);
    void DeleteRange(IEnumerable<T> entities);
    void SaveChanges();
    Task SaveChangesAsync();
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitAsync(CancellationToken cancellationToken = default);
    Task RollbackAsync(CancellationToken cancellationToken = default);
}

public class Repository<T> : IRepository<T> where T : class
{
    private readonly AppDbContext _ctx;
    protected readonly DbSet<T> _dbSet;
    protected IDbContextTransaction? Transaction;
    private bool _disposed;

    public Repository(AppDbContext ctx)
    {
        _ctx = ctx;
        _dbSet = _ctx.Set<T>();
    }

    public IQueryable<T> GetAll() =>
        _dbSet.AsNoTracking();

    public IQueryable<T> ListarPor(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties) =>
        Listar(includeProperties).Where(where);

    public IQueryable<T> ListarEOrdenadosPor<TKey>(Expression<Func<T, bool>> where, Expression<Func<T, TKey>> ordem, bool ascendente = true, params Expression<Func<T, object>>[] includeProperties) =>
        ascendente
            ? ListarPor(where, includeProperties).OrderBy(ordem)
            : ListarPor(where, includeProperties).OrderByDescending(ordem);

    public IQueryable<T> Listar(params Expression<Func<T, object>>[] includeProperties)
    {
        //if (includeProperties.Any())
        //    return Include(_dbSet, includeProperties);

        return _dbSet;
    }

    public IQueryable<T> ListarOrdenadosPor<TKey>(Expression<Func<T, TKey>> ordem, bool ascendente = true, params Expression<Func<T, object>>[] includeProperties) =>
        ascendente
            ? Listar(includeProperties).OrderBy(ordem)
            : Listar(includeProperties).OrderByDescending(ordem);

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await _dbSet.AsNoTracking().ToListAsync(cancellationToken);

    public async Task<IEnumerable<T>> LocalizaPaginaAsync(int numeroPagina, int quantidadeRegistros, CancellationToken cancellationToken = default) =>
        await _dbSet.Skip(quantidadeRegistros * (numeroPagina - 1)).Take(quantidadeRegistros).ToListAsync(cancellationToken);

    //                   If your result set returns 0 records:     If you result set returns 1 record:  If your result set returns many records:
    // SingleOrDefault   returns the default value for the type    returns that record                  throws an exception
    // FirstOrDefault    returns the default value for the type    returns that record                  returns the first record

    public async Task<T?> GetByIdAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default) =>
        await _dbSet.FirstOrDefaultAsync(expression, cancellationToken);

    public async Task<T?> GetByIdNoTrackingAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default) =>
        await _dbSet.AsNoTracking().FirstOrDefaultAsync(expression, cancellationToken);

    public async Task<IEnumerable<T>> GetByWhereAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default) =>
        await _dbSet.Where(predicate).ToListAsync(cancellationToken);

    public async Task<bool> IsUniqueAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default) =>
        await _dbSet.Where(expression).AnyAsync(cancellationToken);

    public async Task<int> GetTotalRegistrosAsync(CancellationToken cancellationToken = default) =>
        await _dbSet.AsNoTracking().CountAsync(cancellationToken);

    public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);

        return entity;
    }

    public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddRangeAsync(entities, cancellationToken);

        return entities;
    }

    public T Update(T entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Update(entity);

        return entity;
    }

    public void Delete(T entity) =>
        _dbSet.Remove(entity);

    public void DeleteRange(IEnumerable<T> entities) =>
        _dbSet.RemoveRange(entities);

    public void SaveChanges()
    {
        _ctx.SaveChanges();
    }

    public async Task SaveChangesAsync()
    {
        await _ctx.SaveChangesAsync();
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        var Transaction = await _ctx.Database.BeginTransactionAsync(cancellationToken);
        return Transaction;
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(Transaction, "_transaction != null");

        await Transaction.CommitAsync(cancellationToken);
    }

    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(Transaction, "_transaction != null");

        await Transaction.RollbackAsync(cancellationToken);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;
        if (disposing) Transaction?.Dispose();
        _disposed = true;
    }

    private async Task DisposeAsync(bool disposing)
    {
        if (_disposed) return;
        if (disposing) if (Transaction is not null) await Transaction.DisposeAsync();
        _disposed = true;
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        // Do not change this code. Put cleanup code in 'DisposeAsync(bool disposing)' method
        await DisposeAsync(true);
        GC.SuppressFinalize(this);
    }
}

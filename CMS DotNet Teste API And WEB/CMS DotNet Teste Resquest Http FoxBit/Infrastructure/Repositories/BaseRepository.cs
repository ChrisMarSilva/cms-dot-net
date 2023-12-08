using CMS_DotNet_Teste_Resquest_Http_FoxBit.Infrastructure.Context;
using CMS_DotNet_Teste_Resquest_Http_FoxBit.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CMS_DotNet_Teste_Resquest_Http_FoxBit.Infrastructure.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    public readonly AppDbContext _ctx; // DbContext // AppDbContext
    protected readonly DbSet<T> _dbSet;

    public BaseRepository(AppDbContext ctx)
    {
        _ctx = ctx;
        _dbSet = _ctx.Set<T>();
    }

    public IQueryable<T> GetAll() //public async Task<IEnumerable<T>> GetAllAsync()
    {
        //return await _dbSet
        //    // .AsNoTracking()
        //    .AsNoTrackingWithIdentityResolution()
        //    .ToListAsync()
        //    .ConfigureAwait(false);

        // return _dbSet.AsNoTracking();
        return _dbSet.AsNoTrackingWithIdentityResolution();
    }

    public async Task<IEnumerable<T>> GetByWhereAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
    {
        // _logger.LogInformation($"{_className}.GetByWhereAsync()");

        return await _dbSet.Where(expression).ToListAsync(cancellationToken);
    }

    public async Task<T> GetByIdAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default) // Guid id
    {
        //If your result set returns 0 records:
        //SingleOrDefault returns the default value for the type(e.g. default for int is 0)
        //FirstOrDefault returns the default value for the type

        //If you result set returns 1 record:
        //SingleOrDefault returns that record
        //FirstOrDefault returns that record

        //If your result set returns many records:
        //SingleOrDefault throws an exception
        //FirstOrDefault returns the first record

        return await _dbSet.SingleOrDefaultAsync(expression, cancellationToken); //?? new T(); // FirstOrDefaultAsync // SingleOrDefaultAsync
    }

    public async Task<T> GetByIdNoTrackingAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default) // Guid id
    {
        //If your result set returns 0 records:
        //SingleOrDefault returns the default value for the type(e.g. default for int is 0)
        //FirstOrDefault returns the default value for the type

        //If you result set returns 1 record:
        //SingleOrDefault returns that record
        //FirstOrDefault returns that record

        //If your result set returns many records:
        //SingleOrDefault throws an exception
        //FirstOrDefault returns the first record

        return await _dbSet.AsNoTracking().SingleOrDefaultAsync(expression, cancellationToken); //?? new T(); // FirstOrDefaultAsync // SingleOrDefaultAsync
    }

    public async Task<bool> IsUniqueAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
    {
        return await _dbSet.Where(expression).AnyAsync(cancellationToken);
    }

    //public async Task<List<T>> LocalizaPaginaAsync(int numeroPagina, int quantidadeRegistros)
    //{
    //    return await _dbSet.Skip(quantidadeRegistros * (numeroPagina - 1)).Take(quantidadeRegistros).ToListAsync();
    //}

    public async Task<int> GetTotalRegistrosAsync()
    {
        return await _dbSet.AsNoTracking().CountAsync();
    }

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

    public T Update(T entity)
    {
        // _ctx.Entry(entity).State = EntityState.Modified;
        _dbSet.Update(entity);
        return entity;
    }

    public bool Remove(T entity)
    {
        _dbSet.Remove(entity);
        return true;
    }

    public bool RemoveRange(IEnumerable<T> entities)
    {
        _dbSet.RemoveRange(entities);
        return true;
    }

    public async Task<bool> RemoveWhereAsync(string tableName, string where)
    {
        await _ctx.Database.ExecuteSqlRawAsync($"DELETE FROM {tableName} WHERE {where}");
        return true;
    }

    public async Task<bool> RemoveAllAsync(string tableName)
    {
        await _ctx.Database.ExecuteSqlRawAsync($"TRUNCATE TABLE {tableName}");
        return true;
    }
}

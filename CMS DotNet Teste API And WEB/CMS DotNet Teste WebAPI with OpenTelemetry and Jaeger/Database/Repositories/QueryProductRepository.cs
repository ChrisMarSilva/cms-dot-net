using Microsoft.EntityFrameworkCore;
using Project.Database.Repositories.Interfaces;
using Project.Domain.Models;

namespace Project.Database.Repositories;

internal partial class QueryRepository : BaseRepository, IQueryRepository
{
    public async Task<ICollection<ProductModel>> FindAllProductAsync(CancellationToken cancellationToken)
    {
        var results = await _context.Products
            .AsNoTracking()
            .OrderBy(d => d.Id)
            //.Skip((pageNumber - 1) * pageSize)
            //.Take(pageSize)
            .ToListAsync(cancellationToken); // AsEnumerable

        return results;
    }

    public async Task<ProductModel?> FindByIdProductAsync(int id, CancellationToken cancellationToken)
    {
        var result = await _context.Products
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync(cancellationToken);

        return result;
    }

    public async Task<bool> ExistByIdProductAsync(int id, CancellationToken cancellationToken)
    {
        var result = await _context.Products
            .AnyAsync(p => p.Id == id, cancellationToken);

        return result;
    }


    public async Task<bool> ExistByNameProductAsync(string name, CancellationToken cancellationToken)
    {
        var result = await _context.Products
            .AnyAsync(p => p.Name == name, cancellationToken);

        return result;
    }
}

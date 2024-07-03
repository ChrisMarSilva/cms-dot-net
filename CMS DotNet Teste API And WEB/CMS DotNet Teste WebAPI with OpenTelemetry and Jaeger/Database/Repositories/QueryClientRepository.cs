using Microsoft.EntityFrameworkCore;
using Project.Database.Context;
using Project.Database.Repositories.Interfaces;

namespace Project.Database.Repositories;

internal partial class QueryRepository : BaseRepository, IQueryRepository
{
    public QueryRepository(ApplicationDbContext context) : base(context) { }

    public async Task<bool> ExistByNameClientAsync(string name, CancellationToken cancellationToken)
    {
        var result = await _context.Clients.AnyAsync(p => p.Name == name, cancellationToken);

        return result;
    }
}

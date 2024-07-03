using Project.Database.Context;
using Project.Database.Repositories.Interfaces;
using Project.Domain.Models;

namespace Project.Database.Repositories;

internal partial class CommandRepository : BaseRepository, ICommandRepository
{
    public CommandRepository(ApplicationDbContext context) : base(context) { }

    public async Task<ClientModel> CreateClientAsync(ClientModel input, CancellationToken cancellationToken)
    {
        await _context.Clients.AddAsync(input, cancellationToken);
        //_context.Set<ClientModel>().Add(input, cancellationToken);

        return input;
    }
}

using Project.Domain.Models;

namespace Project.Database.Repositories.Interfaces;

public partial interface ICommandRepository : IRepository
{
    Task<ClientModel> CreateClientAsync(ClientModel input, CancellationToken cancellationToken);
}

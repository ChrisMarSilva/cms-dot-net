namespace Project.Database.Repositories.Interfaces;

public partial interface IQueryRepository
{
    Task<bool> ExistByNameClientAsync(string name, CancellationToken cancellationToken);
}

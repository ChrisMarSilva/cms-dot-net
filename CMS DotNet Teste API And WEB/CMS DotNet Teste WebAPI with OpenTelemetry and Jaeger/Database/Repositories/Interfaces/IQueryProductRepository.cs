using Project.Domain.Models;

namespace Project.Database.Repositories.Interfaces;

public partial interface IQueryRepository
{
    Task<ICollection<ProductModel>> FindAllProductAsync(CancellationToken cancellationToken);
    Task<ProductModel?> FindByIdProductAsync(int id, CancellationToken cancellationToken);
    Task<bool> ExistByIdProductAsync(int id, CancellationToken cancellationToken);
    Task<bool> ExistByNameProductAsync(string name, CancellationToken cancellationToken);
}

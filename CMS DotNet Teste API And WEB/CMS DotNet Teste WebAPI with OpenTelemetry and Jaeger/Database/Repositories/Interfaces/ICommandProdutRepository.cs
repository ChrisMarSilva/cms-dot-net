using Project.Domain.Models;

namespace Project.Database.Repositories.Interfaces;

public partial interface ICommandRepository : IRepository
{
    Task<ProductModel> CreateProductAsync(ProductModel input, CancellationToken cancellationToken);
    ProductModel UpdateProduct(ProductModel input, CancellationToken cancellationToken);
    bool DeleteProduct(ProductModel input, CancellationToken cancellationToken);
}

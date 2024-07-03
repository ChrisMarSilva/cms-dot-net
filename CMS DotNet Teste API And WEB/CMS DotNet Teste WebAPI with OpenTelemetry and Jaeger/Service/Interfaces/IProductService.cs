using Project.Domain.Dtos.Request;
using Project.Domain.Dtos.Response;

namespace Project.Service.Interfaces;

public partial interface IService
{
    Task<ICollection<ProductResponseDto>> GetAllProductAsync(CancellationToken cancellationToken);
    Task<ProductResponseDto?> GetByIdProductAsync(int id, CancellationToken cancellationToken);
    //Task<CommandResult<object?>> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<ProductResponseDto?> InsertProductAsync(ProductRequestDto request, CancellationToken cancellationToken);
    Task<ProductResponseDto?> UpdateProductAsync(int id, ProductRequestDto request, CancellationToken cancellationToken);
    Task<bool> DeleteProductAsync(int id, CancellationToken cancellationToken);
}

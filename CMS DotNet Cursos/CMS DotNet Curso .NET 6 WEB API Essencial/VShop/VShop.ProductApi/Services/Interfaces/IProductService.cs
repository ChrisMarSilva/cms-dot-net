using VShop.ProductApi.Dtos;

namespace VShop.ProductApi.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetProductsAsync();
    Task<ProductDto> GetProductByIdAsync(int id);
    Task AddProductAsync(ProductDto productDto);
    Task UpdateProductAsync(ProductDto productDto);
    Task RemoveProductAsync(int id);
}

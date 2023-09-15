using VShop.ProductApi.Models;

namespace VShop.ProductApi.Repositories.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<ProductModel>> GetAllAsync();
    Task<ProductModel?> GetByIdAsync(int id);
    Task<ProductModel> CreateAsync(ProductModel product);
    Task<ProductModel> UpdateAsync(ProductModel product);
    Task<ProductModel?> DeleteAsync(int id);
}

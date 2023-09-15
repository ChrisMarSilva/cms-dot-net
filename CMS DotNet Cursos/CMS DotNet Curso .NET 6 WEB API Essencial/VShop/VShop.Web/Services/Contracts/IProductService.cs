using VShop.Web.Models;

namespace VShop.Web.Services.Contracts;

public interface IProductService
{
    Task<IEnumerable<ProductViewModel>?> GetAllProductsAsync(string token);
    Task<ProductViewModel?> FindProductByIdAsync(int id, string token);
    Task<ProductViewModel?> CreateProductAsync(ProductViewModel productVM, string token);
    Task<ProductViewModel?> UpdateProductAsync(ProductViewModel productVM, string token);
    Task<bool> DeleteProductByIdAsync(int id, string token);
}
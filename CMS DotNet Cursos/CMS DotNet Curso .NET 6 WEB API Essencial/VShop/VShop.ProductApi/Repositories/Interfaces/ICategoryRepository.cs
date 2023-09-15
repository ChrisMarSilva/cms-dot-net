using VShop.ProductApi.Models;

namespace VShop.ProductApi.Repositories.Interfaces;

public interface ICategoryRepository
{
    Task<IEnumerable<CategoryModel>> GetAllAsync();
    Task<IEnumerable<CategoryModel>> GetCategoriesProductsAsync();
    Task<CategoryModel?> GetByIdAsync(int id);
    Task<CategoryModel> CreateAsync(CategoryModel category);
    Task<CategoryModel> UpdateAsync(CategoryModel category);
    Task<CategoryModel?> DeleteAsync(int id);
}

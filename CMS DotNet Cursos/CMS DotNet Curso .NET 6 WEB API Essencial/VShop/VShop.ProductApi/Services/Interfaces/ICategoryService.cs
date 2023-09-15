using VShop.ProductApi.Dtos;

namespace VShop.ProductApi.Services.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetCategoriesAsync();
    Task<IEnumerable<CategoryDto>> GetCategoriesProductsAsync();
    Task<CategoryDto> GetCategoryByIdAsync(int id);
    Task AddCategoryAsync(CategoryDto categoryDto);
    Task UpdateCategoryAsync(CategoryDto categoryDto);
    Task RemoveCategoryAsync(int id);
}

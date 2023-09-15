using AutoMapper;
using VShop.ProductApi.Dtos;
using VShop.ProductApi.Models;
using VShop.ProductApi.Repositories.Interfaces;
using VShop.ProductApi.Services.Interfaces;

namespace VShop.ProductApi.Services;

public class CategoryService : ICategoryService
{
    private ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
    {
        var categoriesModel = await _categoryRepository.GetAllAsync();

        var categories = _mapper.Map<IEnumerable<CategoryDto>>(categoriesModel);
        return categories;
    }

    public async Task<IEnumerable<CategoryDto>> GetCategoriesProductsAsync()
    {
        var categoriesModel = await _categoryRepository.GetCategoriesProductsAsync();

        var categories = _mapper.Map<IEnumerable<CategoryDto>>(categoriesModel);
        return categories;
    }

    public async Task<CategoryDto> GetCategoryByIdAsync(int id)
    {
        var categoryModel = await _categoryRepository.GetByIdAsync(id: id);

        var category = _mapper.Map<CategoryDto>(categoryModel);
        return category;
    }

    public async Task AddCategoryAsync(CategoryDto categoryDto)
    {
        var categoryModel = _mapper.Map<CategoryModel>(categoryDto);

        await _categoryRepository.CreateAsync(category: categoryModel);
        categoryDto.CategoryId = categoryModel.CategoryId;
    }

    public async Task UpdateCategoryAsync(CategoryDto categoryDto)
    {
        var categoryModel = _mapper.Map<CategoryModel>(categoryDto);

        await _categoryRepository.UpdateAsync(category: categoryModel);
    }

    public async Task RemoveCategoryAsync(int id)
    {
        var categoryModel = _categoryRepository.GetByIdAsync(id: id).Result;

        if (categoryModel is not null)
            await _categoryRepository.DeleteAsync(id: categoryModel.CategoryId);
    }
}

using Microsoft.EntityFrameworkCore;
using VShop.ProductApi.Context;
using VShop.ProductApi.Models;
using VShop.ProductApi.Repositories.Interfaces;

namespace VShop.ProductApi.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _context;

    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CategoryModel>> GetAllAsync()
    {
        var categories = await _context.Categories
            .ToListAsync();

        return categories;
    }

    public async Task<IEnumerable<CategoryModel>> GetCategoriesProductsAsync()
    {
        var categories = await _context.Categories
            .Include(x => x.Products)
            .ToListAsync();

        return categories;
    }

    public async Task<CategoryModel?> GetByIdAsync(int id)
    {
        var category = await _context.Categories
            .Where(p => p.CategoryId == id)
            .FirstOrDefaultAsync();

        return category;
    }

    public async Task<CategoryModel> CreateAsync(CategoryModel category)
    {
        _context.Categories.Add(entity: category);
        await _context.SaveChangesAsync();

        return category;
    }

    public async Task<CategoryModel> UpdateAsync(CategoryModel category)
    {
        _context.Entry(category).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return category;
    }

    public async Task<CategoryModel?> DeleteAsync(int id)
    {
        var category = await GetByIdAsync(id: id);

        if (category is not null)
        {
            _context.Categories.Remove(entity: category);
            await _context.SaveChangesAsync();
        }

        return category;
    }
}

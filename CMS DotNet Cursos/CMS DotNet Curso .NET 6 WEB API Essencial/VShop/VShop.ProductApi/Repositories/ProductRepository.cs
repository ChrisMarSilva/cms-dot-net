using Microsoft.EntityFrameworkCore;
using VShop.ProductApi.Context;
using VShop.ProductApi.Models;
using VShop.ProductApi.Repositories.Interfaces;

namespace VShop.ProductApi.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductModel>> GetAllAsync()
    {
        var products = await _context.Products
            .Include(c => c.Category)
            .ToListAsync();

        return products;
    }

    public async Task<ProductModel?> GetByIdAsync(int id)
    {
        var product = await _context.Products
            .Include(c => c.Category)
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();

        return product;
    }

    public async Task<ProductModel> CreateAsync(ProductModel product)
    {
        _context.Products.Add(entity: product);
        await _context.SaveChangesAsync();

        return product;
    }

    public async Task<ProductModel> UpdateAsync(ProductModel product)
    {
        _context.Entry(product).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return product;
    }

    public async Task<ProductModel?> DeleteAsync(int id)
    {
        var product = await GetByIdAsync(id: id);

        if (product is not null)
        {
            _context.Products.Remove(entity: product);
            await _context.SaveChangesAsync();
        }

        return product;
    }
}

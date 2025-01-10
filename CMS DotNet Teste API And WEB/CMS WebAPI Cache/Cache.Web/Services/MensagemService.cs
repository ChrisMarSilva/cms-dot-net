using Cache.Web.Database.Contexts;
using Cache.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Cache.Web.Services;

public class MensagemService
{
    private readonly AppDbContext _dbContext;

    public MensagemService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<MensagemModel>> GetAllAsync() => await _dbContext.Mensagens.ToListAsync();

    public async Task<MensagemModel?> GetByIdAsync(int id) => await _dbContext.Mensagens.FindAsync(id);

    public async Task AddAsync(MensagemModel product)
    {
        _dbContext.Mensagens.Add(product);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(MensagemModel product)
    {
        _dbContext.Mensagens.Update(product);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var product = await _dbContext.Mensagens.FindAsync(id);

        if (product != null)
        {
            _dbContext.Mensagens.Remove(product);
            await _dbContext.SaveChangesAsync();
        }
    }
}
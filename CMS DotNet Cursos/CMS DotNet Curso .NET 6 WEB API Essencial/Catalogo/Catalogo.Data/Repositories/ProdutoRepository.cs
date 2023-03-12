using Catalogo.Data.Persistence;
using Catalogo.Data.Repositories.Interfaces;
using Catalogo.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Catalogo.Data.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly ILogger<ProdutoRepository> _logger;
    private readonly AppDbContext _ctx;
    private readonly string? _className;

    public ProdutoRepository(
        ILogger<ProdutoRepository> logger,
        AppDbContext context
        )
    {
        _logger = logger;
        _ctx = context;
        _className = GetType().FullName;

        _logger.LogInformation($"{_className}");
    }

    public async Task<IEnumerable<Produto>> FindAllAsync()
    {
        _logger.LogInformation($"{_className}.FindAllAsync()");

        var pageNumber = 1;
        var pageSize = 100;

        // if (pageNumber == 0) pageNumber = 1;
        // if (pageSize == 0) pageSize = int.MaxValue;

        var results = await _ctx.Produtos
            .AsNoTracking()
            .Where(c => c.DataCadastro >= new DateTime(2000, 1, 1))
            .OrderBy(c => c.DataCadastro)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync()
            .ConfigureAwait(false);

        return results;
    }

    public async Task<Produto> FindByIdAsync(Guid id)
    {
        _logger.LogInformation($"{_className}.FindById()");

        //If your result set returns 0 records:
        //SingleOrDefault returns the default value for the type(e.g. default for int is 0)
        //FirstOrDefault returns the default value for the type

        //If you result set returns 1 record:
        //SingleOrDefault returns that record
        //FirstOrDefault returns that record

        //If your result set returns many records:
        //SingleOrDefault throws an exception
        //FirstOrDefault returns the first record

        var result = await _ctx.Produtos
            .Include(d => d.Categoria)
            .FirstOrDefaultAsync(d => d.Id == id) // FirstOrDefaultAsync // SingleOrDefaultAsync
            ?? new Produto();
        return result;
    }

    public async Task<Produto> CreateAsync(Produto input)
    {
        _logger.LogInformation($"{_className}.Create()");

        await _ctx.Produtos.AddAsync(input);
        return input;
    }

    public Produto Update(Produto input)
    {
        _logger.LogInformation($"{_className}.Update()");

        _ctx.Produtos.Update(input);
        return input;
    }

    public bool Delete(Produto input)
    {
        _logger.LogInformation($"{_className}.Delete()");

        _ctx.Produtos.Remove(input);
        return true;
    }
}

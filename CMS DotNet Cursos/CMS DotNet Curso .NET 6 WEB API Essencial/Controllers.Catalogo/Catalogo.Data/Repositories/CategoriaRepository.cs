using Catalogo.Data.Persistence;
using Catalogo.Data.Repositories.Interfaces;
using Catalogo.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Catalogo.Data.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly ILogger<CategoriaRepository> _logger;
    private readonly AppDbContext _ctx;
    private readonly string? _className;

    public CategoriaRepository(
        ILogger<CategoriaRepository> logger,
        AppDbContext context
        )
    {
        _logger = logger;
        _ctx = context;
        _className = GetType().FullName;

        _logger.LogInformation($"{_className}");
    }

    public async Task<IEnumerable<Categoria>> FindAllAsync()
    {
        _logger.LogInformation($"{_className}.FindAllAsync()");

        var pageNumber = 1;
        var pageSize = 100;

        // if (pageNumber == 0) pageNumber = 1;
        // if (pageSize == 0) pageSize = int.MaxValue;

        var results = await _ctx.Categorias
            //.AsNoTracking()
            .AsNoTrackingWithIdentityResolution()
            .Where(c => c.DataCadastro >= new DateTime(2000, 1, 1))
            .OrderBy(c => c.DataCadastro)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync()
            .ConfigureAwait(false);

        return results;
    }

    public async Task<Categoria> FindByIdAsync(Guid id)
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

        var result = await _ctx.Categorias
            .Include(d => d.Produtos)
            .FirstOrDefaultAsync(d => d.Id == id) // FirstOrDefaultAsync // SingleOrDefaultAsync
            ?? new Categoria(); 
        return result;
    }

    public async Task<Categoria> CreateAsync(Categoria input)
    {
        _logger.LogInformation($"{_className}.Create()");

        await _ctx.Categorias.AddAsync(input);
        return input;
    }

    public Categoria Update(Categoria input)
    {
        _logger.LogInformation($"{_className}.Update()");

        _ctx.Categorias.Update(input);
        return input;
    }

    public bool Delete(Categoria input)
    {
        _logger.LogInformation($"{_className}.Delete()");

        _ctx.Categorias.Remove(input);
        return true;
    }
}

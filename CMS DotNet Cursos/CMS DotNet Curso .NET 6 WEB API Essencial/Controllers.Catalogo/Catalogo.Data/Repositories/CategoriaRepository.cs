using Catalogo.Data.Pagination;
using Catalogo.Data.Persistence;
using Catalogo.Data.Repositories.Interfaces;
using Catalogo.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Catalogo.Data.Repositories;

public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
{
    private readonly ILogger<CategoriaRepository> _logger;
    //private readonly AppDbContext _ctx;
    private readonly string _className;

    public CategoriaRepository(AppDbContext ctx) : base(ctx)
    {
        //_ctx = ctx;
        _className = GetType().FullName;
    }

    public CategoriaRepository(ILogger<CategoriaRepository> logger, AppDbContext ctx) : base(logger, ctx)
    {
        _logger = logger;
        //_ctx = ctx;
        _className = GetType().FullName;

        _logger.LogInformation($"{_className}");
    }

    public async Task<PagedList<Categoria>> GetCategoriasAsync(CategoriasParameters categParams)
    {
        // _logger.LogInformation($"{_className}.GetCategoriasAsync()");

        //return await base.GetAll()
        //    .OrderBy(on => on.Nome)
        //    .Skip((prodParams.PageNumber - 1) * prodParams.PageSize)
        //    .Take(prodParams.PageSize)
        //    .ToListAsync();

        var categorias = base.GetAll().OrderBy(on => on.Nome);

        return await PagedList<Categoria>
            .ToPagedListAsync(categorias, categParams.PageNumber, categParams.PageSize);
    }

    public async Task<IEnumerable<Categoria>> GetAllAsync()
    {
        // _logger.LogInformation($"{_className}.GetAllAsync()");

        // return base.GetAll().Include(x => x.Produtos);

        return await base.GetAll()
            .Where(c => c.DataCadastro >= new DateTime(2000, 1, 1))
            .OrderBy(c => c.DataCadastro)
            .ToListAsync()
            .ConfigureAwait(false);
    }

    //public async Task<Categoria> GetByIdAsync(Guid id)
    //{
    //    // _logger.LogInformation($"{_className}.GetByIdAsync()");
    //
    //    //If your result set returns 0 records:
    //    //SingleOrDefault returns the default value for the type(e.g. default for int is 0)
    //    //FirstOrDefault returns the default value for the type
    //
    //    //If you result set returns 1 record:
    //    //SingleOrDefault returns that record
    //    //FirstOrDefault returns that record
    //
    //    //If your result set returns many records:
    //    //SingleOrDefault throws an exception
    //    //FirstOrDefault returns the first record
    //
    //    var result = await _ctx.Categorias
    //        .Include(d => d.Produtos)
    //        .FirstOrDefaultAsync(d => d.Id == id) // FirstOrDefaultAsync // SingleOrDefaultAsync
    //        ?? new Categoria(); 
    //    return result;
    //}

    //public async Task<Categoria> AddAsync(Categoria input)
    //{
    //    // _logger.LogInformation($"{_className}.Add()");
    //
    //    await _ctx.Categorias.AddAsync(input);
    //    return input;
    //}

    //public Categoria Update(Categoria input)
    //{
    //    // _logger.LogInformation($"{_className}.Update()");
    //
    //    _ctx.Categorias.Update(input);
    //    return input;
    //}

    //public bool Remove(Categoria input)
    //{
    //    // _logger.LogInformation($"{_className}.Remove()");
    //
    //    _ctx.Categorias.Remove(input);
    //    return true;
    //}
}

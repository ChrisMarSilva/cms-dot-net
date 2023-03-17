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

    private const int DefaultPage = 1;
    private const int DefaultPageSize = 10;

    public CategoriaRepository(ILogger<CategoriaRepository> logger, AppDbContext ctx) : base(logger, ctx)
    {
        _logger = logger;
        //_ctx = ctx;
        _className = GetType().FullName;

        _logger.LogInformation($"{_className}");
    }

    public IEnumerable<Categoria> GetCategoriasProdutos()
    {
        return GetAll().Include(x => x.Produtos);
    }

    public async Task<IEnumerable<Categoria>> FindAllAsync()
    {
        _logger.LogInformation($"{_className}.FindAllAsync()");

        var pageNumber = 1;
        var pageSize = 100;

        //page ??= DefaultPage;
        //pageSize ??= DefaultPageSize;
        //if (page <= 0) throw new ArgumentOutOfRangeException(nameof(page));
        //if (pageSize <= 0) throw new ArgumentOutOfRangeException(nameof(pageSize));

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

    //public async Task<Categoria> GetByIdAsync(Guid id)
    //{
    //    _logger.LogInformation($"{_className}.GetByIdAsync()");
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
    //    _logger.LogInformation($"{_className}.Add()");
    //
    //    await _ctx.Categorias.AddAsync(input);
    //    return input;
    //}

    //public Categoria Update(Categoria input)
    //{
    //    _logger.LogInformation($"{_className}.Update()");
    //
    //    _ctx.Categorias.Update(input);
    //    return input;
    //}

    //public bool Remove(Categoria input)
    //{
    //    _logger.LogInformation($"{_className}.Remove()");
    //
    //    _ctx.Categorias.Remove(input);
    //    return true;
    //}
}

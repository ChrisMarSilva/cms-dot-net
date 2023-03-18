using Catalogo.Data.Persistence;
using Catalogo.Data.Repositories.Interfaces;
using Catalogo.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Catalogo.Data.Repositories;

public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
{
    private readonly ILogger<ProdutoRepository> _logger;
    //private readonly AppDbContext _ctx;
    private readonly string _className;
    //private const int DefaultPage = 1;
    //private const int DefaultPageSize = 10;

    public ProdutoRepository(ILogger<ProdutoRepository> logger, AppDbContext ctx) : base(logger, ctx)
    {
        _logger = logger;
        //_ctx = ctx;
        _className = GetType().FullName;

        _logger.LogInformation($"{_className}");
    }

    public async Task<IEnumerable<Produto>> GetAllAsync()
    {
        _logger.LogInformation($"{_className}.GetAllAsync()");

        var pageNumber = 1;
        var pageSize = 100;

        // page ??= DefaultPage;
        // pageSize ??= DefaultPageSize;
        // if (page <= 0) throw new ArgumentOutOfRangeException(nameof(page));
        // if (pageSize <= 0) throw new ArgumentOutOfRangeException(nameof(pageSize));
        // if (pageNumber == 0) pageNumber = 1;
        // if (pageSize == 0) pageSize = int.MaxValue;

        // return base.GetAll().Include(x => x.Gategorias);

        return await base.GetAll()
            .Where(c => c.DataCadastro >= new DateTime(2000, 1, 1))
            .OrderBy(c => c.DataCadastro)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync()
            .ConfigureAwait(false);
    }

    //public async Task<Produto> GetByIdAsync(Guid id)
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
    //    var result = await _ctx.Produtos
    //        .Include(d => d.Categoria)
    //        .FirstOrDefaultAsync(d => d.Id == id) // FirstOrDefaultAsync // SingleOrDefaultAsync
    //        ?? new Produto();
    //    return result;
    //}

    //public async Task<Produto> Add(Produto input)
    //{
    //    _logger.LogInformation($"{_className}.Add()");
    //
    //    await _ctx.Produtos.AddAsync(input);
    //    return input;
    //}

    //public Produto Update(Produto input)
    //{
    //    _logger.LogInformation($"{_className}.Update()");
    //
    //    _ctx.Produtos.Update(input);
    //    return input;
    //}

    //public bool Remove(Produto input)
    //{
    //    _logger.LogInformation($"{_className}.Remove()");
    //
    //    _ctx.Produtos.Remove(input);
    //    return true;
    //}
}

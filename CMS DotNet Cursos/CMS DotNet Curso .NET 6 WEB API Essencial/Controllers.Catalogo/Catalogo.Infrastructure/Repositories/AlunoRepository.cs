using Catalogo.Domain.Interfaces;
using Catalogo.Domain.Models;
using Catalogo.Domain.Pagination;
using Catalogo.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Catalogo.Infrastructure.Repositories;

public class AlunoRepository : BaseRepository<Aluno>, IAlunoRepository
{
    private readonly ILogger<AlunoRepository> _logger;
    //private readonly AppDbContext _ctx;
    private readonly string _className;
    //private const int DefaultPage = 1;
    //private const int DefaultPageSize = 10;

    public AlunoRepository(AppDbContext ctx) : base(ctx)
    {
        //_ctx = ctx;
        _className = GetType().FullName;
    }

    public AlunoRepository(ILogger<AlunoRepository> logger, AppDbContext ctx) : base(logger, ctx)
    {
        _logger = logger;
        //_ctx = ctx;
        _className = GetType().FullName;

        _logger.LogInformation($"{_className}");
    }

    public async Task<PagedList<Aluno>> GetAlunosAsync(AlunosParameters alunoParams)
    {
        // _logger.LogInformation($"{_className}.GeAlunosAsync()");

        //return await base.GetAll()
        //    .OrderBy(on => on.Id)
        //    .Skip((alunoParams.PageNumber - 1) * alunoParams.PageSize)
        //    .Take(alunoParams.PageSize)
        //    .ToListAsync();

        var alunos = base.GetAll().OrderBy(on => on.Id);

        return await PagedList<Aluno>.
            ToPagedListAsync(alunos, alunoParams.PageNumber, alunoParams.PageSize);
    }

    public async Task<IEnumerable<Aluno>> GetAllAsync()
    {
        // _logger.LogInformation($"{_className}.GetAllAsync()");

        return await base.GetAll()
            .Where(c => c.DataCadastro >= new DateTime(2000, 1, 1))
            .OrderBy(c => c.DataCadastro)
            .ToListAsync()
            .ConfigureAwait(false);
    }

}

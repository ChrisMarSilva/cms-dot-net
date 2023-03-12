using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tarefas.Data.Persistence;
using Tarefas.Data.Repositories.Interfaces;
using Tarefas.Domain.Models;

namespace Tarefas.Data.Repositories;

public class TarefaRepository : ITarefaRepository
{
    private readonly ILogger<TarefaRepository> _logger;
    private readonly AppDbContext _ctx;
    private readonly string? _className;

    public TarefaRepository(
        ILogger<TarefaRepository> logger,
        AppDbContext context
        )
    {
        _logger = logger;
        _ctx = context;
        _className = GetType().FullName;

        _logger.LogInformation($"{_className}");
    }

    public async Task<IEnumerable<Tarefa>> FindAllAsync()
    {
        _logger.LogInformation($"{_className}.FindAllAsync()");

        var pageNumber = 1;
        var pageSize = 100;

        // if (pageNumber == 0) pageNumber = 1;
        // if (pageSize == 0) pageSize = int.MaxValue;

        var results = await _ctx.Tarefas
            .AsNoTracking()
            .Where(c => c.DataCadastro >= new DateTime(2000, 1, 1))
            .OrderBy(c => c.DataCadastro)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync()
            .ConfigureAwait(false);

        return results;
    }

    public async Task<Tarefa> FindByIdAsync(Guid id)
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

        var result = await _ctx.Tarefas
            .FirstOrDefaultAsync(d => d.Id == id) // FirstOrDefaultAsync // SingleOrDefaultAsync
            ?? new Tarefa(); 
        return result;
    }

    public async Task<Tarefa> CreateAsync(Tarefa input)
    {
        _logger.LogInformation($"{_className}.Create()");

        await _ctx.Tarefas.AddAsync(input);
        return input;
    }

    public Tarefa Update(Tarefa input)
    {
        _logger.LogInformation($"{_className}.Update()");

        _ctx.Tarefas.Update(input);
        return input;
    }

    public bool Delete(Tarefa input)
    {
        _logger.LogInformation($"{_className}.Delete()");

        _ctx.Tarefas.Remove(input);
        return true;
    }
}

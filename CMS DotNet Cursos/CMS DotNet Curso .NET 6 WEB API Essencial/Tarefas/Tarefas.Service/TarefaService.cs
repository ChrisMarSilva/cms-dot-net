using Microsoft.Extensions.Logging;
using Tarefas.Data.Persistence.Interfaces;
using Tarefas.Data.Repositories.Interfaces;
using Tarefas.Domain.Models;
using Tarefas.Service.Interfaces;

namespace Tarefas.Service;

public class TarefaService : ITarefaService
{
    private readonly ILogger<TarefaService> _logger;
    private ITarefaRepository _tarefaRepo;
    private IUnitofWork _uow;
    private readonly string? _className;

    public TarefaService(
        ILogger<TarefaService> logger,
        ITarefaRepository tarefaRepo,
        IUnitofWork uow
        )
    {
        _logger = logger;
        _tarefaRepo = tarefaRepo ?? throw new ArgumentNullException(nameof(tarefaRepo));
        _uow = uow;
        _className = GetType().FullName;

        _logger.LogInformation($"{_className}");
    }

    public async Task<IEnumerable<Tarefa>> GetAllAsync()
    {
        _logger.LogInformation($"{_className}.GetAllAsync()");
        try
        {
            var results = await _tarefaRepo.FindAllAsync();

            return results;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{_className}.GetAllAsync(Erro: {ex.Message})");
            throw;
        }
    }

    public async Task<Tarefa> GetByIdAsync(Guid id)
    {
        _logger.LogInformation($"{_className}.GetByIdAsync()");
        try
        {
            var result = await _tarefaRepo.FindByIdAsync(id);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{_className}.GetByIdAsync(Erro: {ex.Message})");
            throw;
        }
    }

    public async Task<Tarefa> InsertAsync(Tarefa input)
    {
        _logger.LogInformation($"{_className}.InsertAsync()");
        try
        {
            var result = await _tarefaRepo.CreateAsync(input);

            if (result is null || result?.Id == Guid.Empty)
                return new Tarefa();

            var resultCommit = await _uow.CommitAsync();

            if (!resultCommit)
                return new Tarefa();

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{_className}.InsertAsync(Erro: {ex.Message})");
            throw;
        }
    }

    public async Task<Tarefa> UpdateAsync(Guid id, Tarefa input)
    {
        _logger.LogInformation($"{_className}.UpdateAsync()");
        try
        {
            var result = await _tarefaRepo.FindByIdAsync(id);

            if (result == null || result?.Id == Guid.Empty)
                return new Tarefa();

            result.Update(
                atividade: input.Atividade,
                status: input.Status
            );

            result = _tarefaRepo.Update(result);

            if (result is null || result?.Id == Guid.Empty)
                return new Tarefa();

            var resultCommit = await _uow.CommitAsync();

            if (!resultCommit)
                return new Tarefa();

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{_className}.UpdateAsync(Erro: {ex.Message})");
            throw;
        }
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        _logger.LogInformation($"{_className}.DeleteAsync()");
        try
        {
            var result = await _tarefaRepo.FindByIdAsync(id);

            if (result is null || result?.Id == Guid.Empty)
                return false;

            var status = _tarefaRepo.Delete(result);

            if (!status)
                return false;

            var resultCommit = await _uow.CommitAsync();

            if (!resultCommit)
                return false;

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{_className}.DeleteAsync(Erro: {ex.Message})");
            throw; // return false;
        }
    }
}

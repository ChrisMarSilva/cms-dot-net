using Catalogo.Data.Persistence.Interfaces;
using Catalogo.Data.Repositories.Interfaces;
using Catalogo.Domain.Models;
using Catalogo.Service.Interfaces;
using Microsoft.Extensions.Logging;

namespace Catalogo.Service;

public class CategoriaService : ICategoriaService
{
    private readonly ILogger<CategoriaService> _logger;
    private ICategoriaRepository _categRepo;
    private IUnitofWork _uow;
    private readonly string? _className;

    public CategoriaService(
        ILogger<CategoriaService> logger,
        ICategoriaRepository categRepo,
        IUnitofWork uow
        )
    {
        _logger = logger;
        _categRepo = categRepo ?? throw new ArgumentNullException(nameof(categRepo));
        _uow = uow;
        _className = GetType().FullName;

        _logger.LogInformation($"{_className}");
    }

    public async Task<IEnumerable<Categoria>> GetAllAsync()
    {
        _logger.LogInformation($"{_className}.GetAllAsync()");
        try
        {
            var results = await _categRepo.FindAllAsync();

            return results;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{_className}.GetAllAsync(Erro: {ex.Message})");
            throw;
        }
    }

    public async Task<Categoria> GetByIdAsync(Guid id)
    {
        _logger.LogInformation($"{_className}.GetByIdAsync()");
        try
        {
            var result = await _categRepo.FindByIdAsync(id);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{_className}.GetByIdAsync(Erro: {ex.Message})");
            throw;
        }
    }

    public async Task<Categoria> InsertAsync(Categoria input)
    {
        _logger.LogInformation($"{_className}.InsertAsync()");
        try
        {
            var result = await _categRepo.CreateAsync(input);

            if (result is null || result?.Id == Guid.Empty)
                return new Categoria();

            var resultCommit = await _uow.CommitAsync();

            if (!resultCommit)
                return new Categoria();

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{_className}.InsertAsync(Erro: {ex.Message})");
            throw;
        }
    }

    public async Task<Categoria> UpdateAsync(Guid id, Categoria input)
    {
        _logger.LogInformation($"{_className}.UpdateAsync()");
        try
        {
            var result = await _categRepo.FindByIdAsync(id);

            if (result == null || result?.Id == Guid.Empty)
                return new Categoria();

            result.Update(
                nome: input.Nome, 
                imagemUrl: input.ImagemUrl
            );

            result = _categRepo.Update(result);

            if (result is null || result?.Id == Guid.Empty)
                return new Categoria();

            var resultCommit = await _uow.CommitAsync();

            if (!resultCommit)
                return new Categoria();

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
            var result = await _categRepo.FindByIdAsync(id);

            if (result is null || result?.Id == Guid.Empty)
                return false;

            var status = _categRepo.Delete(result);

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

using Catalogo.Data.Persistence.Interfaces;
using Catalogo.Data.Repositories.Interfaces;
using Catalogo.Domain.Models;
using Catalogo.Service.Interfaces;
using Microsoft.Extensions.Logging;

namespace Catalogo.Service;

public class ProdutoService : IProdutoService
{
    private readonly ILogger<ProdutoService> _logger;
    private IProdutoRepository _prodRepo;
    private IUnitofWork _uow;
    private readonly string? _className;

    public ProdutoService(
        ILogger<ProdutoService> logger,
        IProdutoRepository produtoRepo,
        IUnitofWork uow
        )
    {
        _logger = logger;
        _prodRepo = produtoRepo ?? throw new ArgumentNullException(nameof(produtoRepo));
        _uow = uow;
        _className = GetType().FullName;

        _logger.LogInformation($"{_className}");
    }

    public async Task<IEnumerable<Produto>> GetAllAsync()
    {
        _logger.LogInformation($"{_className}.GetAllAsync()");
        try
        {
            var results = await _prodRepo.FindAllAsync();

            return results;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{_className}.GetAllAsync(Erro: {ex.Message})");
            throw;
        }
    }

    public async Task<Produto> GetByIdAsync(Guid id)
    {
        _logger.LogInformation($"{_className}.GetByIdAsync()");
        try
        {
            var result = await _prodRepo.FindByIdAsync(id);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{_className}.GetByIdAsync(Erro: {ex.Message})");
            throw;
        }
    }

    public async Task<Produto> InsertAsync(Produto input)
    {
        _logger.LogInformation($"{_className}.InsertAsync()");
        try
        {
            var result = await _prodRepo.CreateAsync(input);

            if (result is null || result?.Id == Guid.Empty)
                return new Produto();

            var resultCommit = await _uow.CommitAsync();

            if (!resultCommit)
                return new Produto();

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{_className}.InsertAsync(Erro: {ex.Message})");
            throw;
        }
    }

    public async Task<Produto> UpdateAsync(Guid id, Produto input)
    {
        _logger.LogInformation($"{_className}.UpdateAsync()");
        try
        {
            var result = await _prodRepo.FindByIdAsync(id);

            if (result == null || result?.Id == Guid.Empty)
                return new Produto();

            result.Update(
                nome: input.Nome,
                preco: input.Preco,
                estoque: input.Estoque,
                categoriaId: input.CategoriaId,
                descricao: input.Descricao,
                imagemUrl: input.ImagemUrl
            );

            result = _prodRepo.Update(result);

            if (result is null || result?.Id == Guid.Empty)
                return new Produto();

            var resultCommit = await _uow.CommitAsync();

            if (!resultCommit)
                return new Produto();

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
            var result = await _prodRepo.FindByIdAsync(id);

            if (result is null || result?.Id == Guid.Empty)
                return false;

            var status = _prodRepo.Delete(result);

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

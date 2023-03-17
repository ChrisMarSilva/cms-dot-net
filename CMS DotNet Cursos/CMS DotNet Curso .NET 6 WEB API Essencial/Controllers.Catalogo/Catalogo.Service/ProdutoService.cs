using AutoMapper;
using Catalogo.Data.Persistence.Interfaces;
using Catalogo.Domain.Models;
using Catalogo.Service.Interfaces;
using Microsoft.Extensions.Logging;

namespace Catalogo.Service;

public class ProdutoService : IProdutoService
{
    private readonly ILogger<ProdutoService> _logger;
    // private readonly IProdutoRepository _prodRepo;
    private IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly string? _className;

    public ProdutoService(
        ILogger<ProdutoService> logger,
        //IProdutoRepository produtoRepo,
        IUnitOfWork uow,
        IMapper mapper
        )
    {
        _logger = logger;
        //_prodRepo = produtoRepo ?? throw new ArgumentNullException(nameof(IProdutoRepository));
        _uow = uow;
        _mapper = mapper;
        _className = GetType().FullName;

        _logger.LogInformation($"{_className}");
    }

    public async Task<IEnumerable<Produto>> GetAllAsync()
    {
        _logger.LogInformation($"{_className}.GetAllAsync()");
        try
        {
            // var results = await _prodRepo.FindAllAsync();
            var results = await _uow.Produtos.FindAllAsync();
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
            // var result = await _prodRepo.GetByIdAsync(id);
            var result = await _uow.Produtos.GetByIdNoTrackingAsync(p => p.Id == id);
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
            //var result = await _prodRepo.CreateAsync(input);
            var result = await _uow.Produtos.AddAsync(input);

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
            // var result = await _prodRepo.GetByIdAsync(id);
            var result = await _uow.Produtos.GetByIdAsync(p => p.Id == id);

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

            //result = _prodRepo.Update(result);
            result = _uow.Produtos.Update(result);

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
            //var result = await _prodRepo.GetByIdAsync(id);
            var result = await _uow.Produtos.GetByIdAsync(p => p.Id == id);

            if (result is null || result?.Id == Guid.Empty)
                return false;

            //var status = _prodRepo.Delete(result);
            var status = _uow.Produtos.Remove(result);

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

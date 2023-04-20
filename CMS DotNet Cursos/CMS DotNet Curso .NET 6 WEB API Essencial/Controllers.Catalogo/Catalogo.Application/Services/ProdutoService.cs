using AutoMapper;
using Catalogo.Application.Dtos;
using Catalogo.Application.Interfaces;
using Catalogo.Domain.Models;
using Catalogo.Domain.Pagination;
using Catalogo.Infrastructure.Context.Interfaces;
using Microsoft.Extensions.Logging;

namespace Catalogo.Application.Services;

public class ProdutoService : IProdutoService
{
    private readonly ILogger<ProdutoService> _logger;
    // private readonly IProdutoRepository _prodRepo;
    private IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly string _className;

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

    //public async Task<IEnumerable<ProdutoResponseDTO>> GetAllAsync()
    public async Task<(dynamic, IEnumerable<ProdutoResponseDTO>)> GetAllAsync(ProdutosParameters? prodParams)
    {
        _logger.LogInformation($"{_className}.GetAllAsync()");
        try
        {
            // var results = await _prodRepo.FindAllAsync();
            // var results = await _uow.Produtos.GetAllAsync();
            var results = await _uow.Produtos.GetProdutosAsync(prodParams);
            var metadata = new { results.TotalCount, results.PageSize, results.CurrentPage, results.TotalPages, results.HasNext, results.HasPrevious };

            return (metadata, _mapper.Map<List<ProdutoResponseDTO>>(results));
            // return results;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{_className}.GetAllAsync(Erro: {ex.Message})");
            throw;
        }
    }

    public async Task<ProdutoResponseDTO> GetByIdAsync(Guid id)
    {
        _logger.LogInformation($"{_className}.GetByIdAsync()");
        try
        {
            // var result = await _prodRepo.GetByIdAsync(id);
            var result = await _uow.Produtos.GetByIdNoTrackingAsync(p => p.Id == id);

            if (result == null)
                return null;

            return _mapper.Map<ProdutoResponseDTO>(result);
            //return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{_className}.GetByIdAsync(Erro: {ex.Message})");
            throw;
        }
    }

    public async Task<ProdutoResponseDTO> InsertAsync(ProdutoRequestDTO input)
    {
        _logger.LogInformation($"{_className}.InsertAsync()");
        try
        {
            var prod = _mapper.Map<Produto>(input);

            //var result = await _prodRepo.CreateAsync(prod);
            var result = await _uow.Produtos.AddAsync(prod);

            if (result is null || result?.Id == Guid.Empty)
                return null; // new Produto();

            var resultCommit = await _uow.CommitAsync();

            if (!resultCommit)
                throw new Exception("Erro ao commitar inclusção"); // return null; // new Produto();


            return _mapper.Map<ProdutoResponseDTO>(result);
            // return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{_className}.InsertAsync(Erro: {ex.Message})");
            throw;
        }
    }

    public async Task<ProdutoResponseDTO> UpdateAsync(Guid id, ProdutoRequestDTO input)
    {
        _logger.LogInformation($"{_className}.UpdateAsync()");
        try
        {
            //if (id != input.Id)
            //    return null; // new Produto();

            // var Prod = await _prodRepo.GetByIdAsync(id);
            var Prod = await _uow.Produtos.GetByIdAsync(p => p.Id == id);

            if (Prod == null || Prod?.Id == Guid.Empty)
                return null; // new Produto();

            Prod.Update(
                nome: input.Nome,
                preco: input.Preco,
                estoque: input.Estoque,
                categoriaId: input.CategoriaId,
                descricao: input.Descricao,
                imagemUrl: input.ImagemUrl
            );

            //var result = _prodRepo.Update(Prod);
            var result = _uow.Produtos.Update(Prod);

            if (result is null || result?.Id == Guid.Empty)
                return null; // new Produto();

            var resultCommit = await _uow.CommitAsync();

            if (!resultCommit)
                throw new Exception("Erro ao commitar alteração"); // return null; // new Produto();


            return _mapper.Map<ProdutoResponseDTO>(result);
            // return result;
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
                throw new Exception("Erro ao commitar exclusão"); // return false;

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{_className}.DeleteAsync(Erro: {ex.Message})");
            throw; // return false;
        }
    }
}

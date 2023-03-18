using AutoMapper;
using Catalogo.Data.Persistence.Interfaces;
using Catalogo.Domain.Dtos;
using Catalogo.Domain.Models;
using Catalogo.Service.Interfaces;
using Microsoft.Extensions.Logging;

namespace Catalogo.Service;

public class CategoriaService : ICategoriaService
{
    private readonly ILogger<CategoriaService> _logger;
    // private readonly ICategoriaRepository _categRepo;
    private IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly string _className;

    public CategoriaService(
        ILogger<CategoriaService> logger,
        //ICategoriaRepository categRepo,
        IUnitOfWork uow,
        IMapper mapper
        )
    {
        _logger = logger;
        // _categRepo = categRepo ?? throw new ArgumentNullException(nameof(ICategoriaRepository));
        _uow = uow;
        _mapper = mapper;
        _className = GetType().FullName;

        _logger.LogInformation($"{_className}");
    }

    public async Task<IEnumerable<CategoriaResponseDTO>> GetAllAsync()
    {
        _logger.LogInformation($"{_className}.GetAllAsync()");
        try
        {
            //var results = await _categRepo.FindAllAsync();
            var results = await _uow.Categorias.GetAllAsync();

            return results.Select(c => new CategoriaResponseDTO{ Id = c.Id, Nome = c.Nome, ImagemUrl = c.ImagemUrl });
            // return _mapper.Map<List<CategoriaResponseDTO>>(results);
            // return results;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{_className}.GetAllAsync(Erro: {ex.Message})");
            throw;
        }
    }

    public async Task<CategoriaResponseDTO> GetByIdAsync(Guid id)
    {
        _logger.LogInformation($"{_className}.GetByIdAsync()");
        try
        {
            // var result = await _categRepo.GetByIdAsync(id);
            var result = await _uow.Categorias.GetByIdNoTrackingAsync(c => c.Id == id);

            if (result == null)
                return null;

            return new CategoriaResponseDTO { Id = result.Id, Nome = result.Nome, ImagemUrl = result.ImagemUrl };
            // return _mapper.Map<CategoriaResponseDTO>(result);
            //return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{_className}.GetByIdAsync(Erro: {ex.Message})");
            throw;
        }
    }

    public async Task<CategoriaResponseDTO> InsertAsync(CategoriaRequestDTO input)
    {
        _logger.LogInformation($"{_className}.InsertAsync()");
        try
        {
            //var categ = _mapper.Map<Categoria>(input);
            var categ = new Categoria(input.Nome, input.ImagemUrl);

            //var result = await _categRepo.CreateAsync(categ);
            var result = await _uow.Categorias.AddAsync(categ);

            if (result is null || result?.Id == Guid.Empty)
                return null; // new Categoria();

            var resultCommit = await _uow.CommitAsync();

            if (!resultCommit)
                return null; // new Categoria();

            return new CategoriaResponseDTO { Id = result.Id, Nome = result.Nome, ImagemUrl = result.ImagemUrl };
            // return _mapper.Map<CategoriaResponseDTO>(result);
            // return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{_className}.InsertAsync(Erro: {ex.Message})");
            throw;
        }
    }

    public async Task<CategoriaResponseDTO> UpdateAsync(Guid id, CategoriaRequestDTO input)
    {
        _logger.LogInformation($"{_className}.UpdateAsync()");
        try
        {
            //if (id != input.Id)
            //    return null; // new Categoria();

            //var categ = await _categRepo.GetByIdAsync(id);
            var categ = await _uow.Categorias.GetByIdAsync(c => c.Id == id);

            if (categ == null || categ?.Id == Guid.Empty)
                return null; // new Categoria();

            categ.Update(
                nome: input.Nome, 
                imagemUrl: input.ImagemUrl
            );

            //var result = _categRepo.Update(result);
            var result = _uow.Categorias.Update(categ);

            if (result is null || result?.Id == Guid.Empty)
                return null; // new Categoria();

            var resultCommit = await _uow.CommitAsync();

            if (!resultCommit)
                return null; // new Categoria();


            return new CategoriaResponseDTO { Id = result.Id, Nome = result.Nome, ImagemUrl = result.ImagemUrl };
            //return _mapper.Map<CategoriaResponseDTO>(result);
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
            //var result = await _categRepo.GetByIdAsync(id);
            var result = await _uow.Categorias.GetByIdAsync(c => c.Id == id);

            if (result is null || result?.Id == Guid.Empty)
                return false;

            // var status = _categRepo.Delete(result);
            var status = _uow.Categorias.Remove(result);

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

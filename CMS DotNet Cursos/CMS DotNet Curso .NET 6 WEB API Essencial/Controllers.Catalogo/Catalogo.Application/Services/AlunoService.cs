using AutoMapper;
using Catalogo.Application.Dtos;
using Catalogo.Application.Interfaces;
using Catalogo.Domain.Models;
using Catalogo.Domain.Pagination;
using Catalogo.Infrastructure.Context.Interfaces;
using Microsoft.Extensions.Logging;

namespace Catalogo.Application.Services;

public class AlunoService : IAlunoService
{
    private readonly ILogger<AlunoService> _logger;
    // private readonly IAlunoRepository _alunoRepo;
    private IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly string _className;

    public AlunoService(
        ILogger<AlunoService> logger,
        //IAlunoRepository alunoRepo,
        IUnitOfWork uow,
        IMapper mapper
        )
    {
        _logger = logger;
        //_alunoRepo = alunoRepo ?? throw new ArgumentNullException(nameof(IAlunoRepository));
        _uow = uow;
        _mapper = mapper;
        _className = GetType().FullName;

        _logger.LogInformation($"{_className}");
    }

    //public async Task<IEnumerableAlunoResponseDTO>> GetAllAsync()
    public async Task<(dynamic, IEnumerable<AlunoResponseDTO>)> GetAllAsync(AlunosParameters? alunoParams)
    {
        _logger.LogInformation($"{_className}.GetAllAsync()");
        try
        {
            // var results = await _alunoRepo.FindAllAsync();
            // var results = await _uow.Alunos.GetAllAsync();
            var results = await _uow.Alunos.GetAlunosAsync(alunoParams);
            var metadata = new { results.TotalCount, results.PageSize, results.CurrentPage, results.TotalPages, results.HasNext, results.HasPrevious };

            return (metadata, _mapper.Map<List<AlunoResponseDTO>>(results));
            // return results;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{_className}.GetAllAsync(Erro: {ex.Message})");
            throw;
        }
    }

    public async Task<AlunoResponseDTO> GetByIdAsync(Guid id)
    {
        _logger.LogInformation($"{_className}.GetByIdAsync()");
        try
        {
            // var result = await _alunoRepo.GetByIdAsync(id);
            var result = await _uow.Alunos.GetByIdNoTrackingAsync(p => p.Id == id);

            if (result == null)
                return null;

            return _mapper.Map<AlunoResponseDTO>(result);
            //return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{_className}.GetByIdAsync(Erro: {ex.Message})");
            throw;
        }
    }

    public async Task<IEnumerable<AlunoResponseDTO>> GetByNomeAsync(string nome)
    {
        _logger.LogInformation($"{_className}.GetByNomeAsync()");
        try
        {
            if (!string.IsNullOrWhiteSpace(nome))
            {
                var results = await _uow.Alunos.GetByWhereAsync(a => a.Nome.Contains(nome));

                if (results == null)
                    return null;

                return _mapper.Map<List<AlunoResponseDTO>>(results);
            }
            else
            {
                var results = await _uow.Alunos.GetAllAsync();

                if (results == null)
                    return null;

                return _mapper.Map<List<AlunoResponseDTO>>(results);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"{_className}.GetByNomeAsync(Erro: {ex.Message})");
            throw;
        }
    }

    public async Task<AlunoResponseDTO> InsertAsync(AlunoRequestDTO input)
    {
        _logger.LogInformation($"{_className}.InsertAsync()");
        try
        {
            var aluno = _mapper.Map<Aluno>(input);

            //var result = await _alunoRepo.CreateAsync(aluno);
            var result = await _uow.Alunos.AddAsync(aluno);

            if (result is null || result?.Id == Guid.Empty)
                return null; // new Aluno();

            var resultCommit = await _uow.CommitAsync();

            if (!resultCommit)
                throw new Exception("Erro ao commitar inclusção"); // return null; // new Aluno();


            return _mapper.Map<AlunoResponseDTO>(result);
            // return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{_className}.InsertAsync(Erro: {ex.Message})");
            throw;
        }
    }

    public async Task<AlunoResponseDTO> UpdateAsync(Guid id, AlunoRequestDTO input)
    {
        _logger.LogInformation($"{_className}.UpdateAsync()");
        try
        {
            //if (id != input.Id)
            //    return null; // new Aluno();

            // var aluno = await _alunoRepo.GetByIdAsync(id);
            var aluno = await _uow.Alunos.GetByIdAsync(p => p.Id == id);

            if (aluno == null || aluno?.Id == Guid.Empty)
                return null; // new Aluno();

            aluno.Update(
                nome: input.Nome,
                email: input.Email,
                idade: input.Idade
            );

            //var result = _alunoRepo.Update(aluno);
            var result = _uow.Alunos.Update(aluno);

            if (result is null || result?.Id == Guid.Empty)
                return null; // new Aluno();

            var resultCommit = await _uow.CommitAsync();

            if (!resultCommit)
                throw new Exception("Erro ao commitar alteração"); // return null; // new Aluno();


            return _mapper.Map<AlunoResponseDTO>(result);
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
            //var result = await _alunoRepo.GetByIdAsync(id);
            var result = await _uow.Alunos.GetByIdAsync(p => p.Id == id);

            if (result is null || result?.Id == Guid.Empty)
                return false;

            //var status = _alunoRepo.Delete(result);
            var status = _uow.Alunos.Remove(result);

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

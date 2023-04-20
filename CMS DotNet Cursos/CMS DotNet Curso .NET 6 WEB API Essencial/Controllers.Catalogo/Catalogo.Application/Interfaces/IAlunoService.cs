using Catalogo.Application.Dtos;
using Catalogo.Domain.Pagination;

namespace Catalogo.Application.Interfaces;

public interface IAlunoService
{
    Task<(dynamic, IEnumerable<AlunoResponseDTO>)> GetAllAsync(AlunosParameters prodParams);
    Task<AlunoResponseDTO> GetByIdAsync(Guid id);
    Task<IEnumerable<AlunoResponseDTO>> GetByNomeAsync(string nome);
    Task<AlunoResponseDTO> InsertAsync(AlunoRequestDTO request);
    Task<AlunoResponseDTO> UpdateAsync(Guid id, AlunoRequestDTO request);
    Task<bool> DeleteAsync(Guid id);
}

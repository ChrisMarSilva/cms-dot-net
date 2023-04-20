using Catalogo.Application.Dtos;
using Catalogo.Domain.Pagination;

namespace Catalogo.Application.Interfaces;

public interface ICategoriaService
{
    //Task<IEnumerable<CategoriaResponseDTO>> GetAllAsync();
    Task<(dynamic, IEnumerable<CategoriaResponseDTO>)> GetAllAsync(CategoriasParameters categParams);
    Task<(int, int, IEnumerable<CategoriaResponseDTO>)> GetPaginacaoAsync(int pag, int reg);
    Task<CategoriaResponseDTO> GetByIdAsync(Guid id);
    Task<CategoriaResponseDTO> InsertAsync(CategoriaRequestDTO request);
    Task<CategoriaResponseDTO> UpdateAsync(Guid id, CategoriaRequestDTO request);
    Task<bool> DeleteAsync(Guid id);
}

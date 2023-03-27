using Catalogo.Data.Pagination;
using Catalogo.Domain.Dtos;

namespace Catalogo.Service.Interfaces;

public interface ICategoriaService
{
    //Task<IEnumerable<CategoriaResponseDTO>> GetAllAsync();
    Task<(dynamic, IEnumerable<CategoriaResponseDTO>)> GetAllAsync(CategoriasParameters categParams);
    Task<CategoriaResponseDTO> GetByIdAsync(Guid id);
    Task<CategoriaResponseDTO> InsertAsync(CategoriaRequestDTO request);
    Task<CategoriaResponseDTO> UpdateAsync(Guid id, CategoriaRequestDTO request);
    Task<bool> DeleteAsync(Guid id);
}

using Catalogo.Domain.Dtos;

namespace Catalogo.Service.Interfaces;

public interface IProdutoService
{
    Task<IEnumerable<ProdutoResponseDTO>> GetAllAsync();
    Task<ProdutoResponseDTO> GetByIdAsync(Guid id);
    Task<ProdutoResponseDTO> InsertAsync(ProdutoRequestDTO input);
    Task<ProdutoResponseDTO> UpdateAsync(Guid id, ProdutoRequestDTO input);
    Task<bool> DeleteAsync(Guid id);
}

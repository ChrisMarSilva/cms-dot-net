using Catalogo.Data.Pagination;
using Catalogo.Domain.Dtos;

namespace Catalogo.Service.Interfaces;

public interface IProdutoService
{
    //Task<IEnumerable<ProdutoResponseDTO>> GetAllAsync();
    Task<(dynamic, IEnumerable<ProdutoResponseDTO>)> GetAllAsync(ProdutosParameters prodParams);
    Task<ProdutoResponseDTO> GetByIdAsync(Guid id);
    Task<ProdutoResponseDTO> InsertAsync(ProdutoRequestDTO request);
    Task<ProdutoResponseDTO> UpdateAsync(Guid id, ProdutoRequestDTO request);
    Task<bool> DeleteAsync(Guid id);
}

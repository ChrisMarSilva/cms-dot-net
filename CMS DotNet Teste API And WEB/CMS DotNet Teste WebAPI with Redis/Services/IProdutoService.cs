using CMS_DotNet_Teste_WebAPI_with_Redis.Dtos;

namespace CMS_DotNet_Teste_WebAPI_with_Redis.Services;

public interface IProdutoService
{
    Task<IEnumerable<ProdutoResponseDto>> GetAllAsync();
    Task<ProdutoResponseDto> GetByIdAsync(Guid id);
    Task<ProdutoResponseDto> InsertAsync(ProdutoRequestDto request);
    Task<ProdutoResponseDto> UpdateAsync(Guid id, ProdutoRequestDto request);
    Task<bool> DeleteAsync(Guid id);
}

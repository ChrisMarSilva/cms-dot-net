using CMS_DotNet_Teste_WebAPI_with_Redis.Models;

namespace CMS_DotNet_Teste_WebAPI_with_Redis.Repositories;

public interface IProdutoRepository
{
    Task<List<Produto>> GetAllAsync();
    Task<Produto> GetByIdAsync(Guid id);
    Task<Produto> CreateAsync(Produto input);
    Produto Update(Produto input);
    bool Delete(Produto input);
}

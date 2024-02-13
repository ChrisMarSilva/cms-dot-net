using CMS_DotNet_Teste_Resquest_Http_FoxBit.Domain.Models;

namespace CMS_DotNet_Teste_Resquest_Http_FoxBit.Infrastructure.Repositories.Interfaces;

public interface ITradeRepository : IBaseRepository<TradeModel>
{
    Task<IEnumerable<TradeModel>> GetAllAsync();
}

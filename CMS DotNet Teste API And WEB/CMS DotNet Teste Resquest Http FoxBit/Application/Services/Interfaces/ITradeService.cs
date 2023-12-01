using CMS_DotNet_Teste_Resquest_Http_FoxBit.Application.Dtos;
using CMS_DotNet_Teste_Resquest_Http_FoxBit.Domain.Models;

namespace CMS_DotNet_Teste_Resquest_Http_FoxBit.Application.Services.Interfaces;

public interface ITradeService
{
    Task<IEnumerable<TradeModel>> GetAllAsync();
    Task<TradeModel> GetByIdAsync(int id);
    Task<TradeModel> InsertAsync(TradeModel request);
    Task<TradeModel> UpdateAsync(int id, TradeModel request);
    Task<bool> DeleteAsync(int id);
}

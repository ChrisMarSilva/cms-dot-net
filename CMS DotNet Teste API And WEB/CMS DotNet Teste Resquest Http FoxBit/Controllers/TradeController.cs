using CMS_DotNet_Teste_Resquest_Http_FoxBit.Application.Services.Interfaces;
using CMS_DotNet_Teste_Resquest_Http_FoxBit.Domain.Models;
using Microsoft.Extensions.Logging;

namespace CMS_DotNet_Teste_Resquest_Http_FoxBit.Controllers;

public class TradeController
{
    private readonly ILogger<TradeController> _logger;
    private readonly ITradeService _tradeService;

    public TradeController() { }

    public TradeController(ILogger<TradeController> logger, ITradeService tradeService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _tradeService = tradeService ?? throw new ArgumentNullException(nameof(tradeService));
    }

    public async Task<IEnumerable<TradeModel>?> GetAll()
    {
        try
        {
            var response = await _tradeService.GetAllAsync();

            if (response is null || !response.Any())
                return null;

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError($"GetAll(Erro : {ex.Message})");
            return null;
        }
    }

    public async Task<TradeModel?> GetById(int id)
    {
        try
        {
            var response = await _tradeService.GetByIdAsync(id);

            if (response is null)
                _logger.LogError($"No record found");

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError($".GetById(Erro: {ex.Message})");
            return null;
        }
    }

    public async Task<TradeModel?> Post(TradeModel request)
    {
        try
        {
            var response = await _tradeService.InsertAsync(request);

            if (response is null)
                _logger.LogError($"No record");

            return response;
        }
        catch (Exception ex)
        {
             _logger.LogError($"Post(Erro: {ex.Message})");
            return null;
        }
    }

    public async Task<TradeModel?> Update(int id, TradeModel request)
    {
        try
        {
            var response = await _tradeService.UpdateAsync(id, request);

            if (response is null)
                _logger.LogError($"No record found");

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Update(Erro: {ex.Message})");
            return null;
        }
    }

    public async Task<bool> Delete(int id)
    {
        try
        {
            var response = await _tradeService.DeleteAsync(id);

            if (!response)
                _logger.LogError($"No record found");

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Delete(Erro: {ex.Message})");
            return false;
        }
    }
}

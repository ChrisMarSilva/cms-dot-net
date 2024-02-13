using CMS_DotNet_Teste_Resquest_Http_FoxBit.Application.Dtos;
using CMS_DotNet_Teste_Resquest_Http_FoxBit.Application.Services.Interfaces;
using CMS_DotNet_Teste_Resquest_Http_FoxBit.Domain.Models;
using CMS_DotNet_Teste_Resquest_Http_FoxBit.Infrastructure.Context.Interfaces;
using CMS_DotNet_Teste_Resquest_Http_FoxBit.Shared;
using System.Text.Json;

namespace CMS_DotNet_Teste_Resquest_Http_FoxBit.Application.Services;

public class MarketService : IMarketService
{
    private IUnitOfWork _uow;

    public MarketService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task GetMarketsAsync()
    {
        var requestUri = "/rest/v3/markets";
        var response = await Uteis.GetRequestWithoutAuth(requestUri: requestUri);

        if (string.IsNullOrEmpty(response))
            return;

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var markets = JsonSerializer.Deserialize<IEnumerable<MarketResponseDto>?>(json: response, options: options); // MarketRootResponseDto // MarketResponseDto

        if (markets == null)
            return;

        await _uow.Markets.RemoveAllAsync("TbTnBFoxbit_Markets");

        int index = 0;
        var entities = new List<MarketModel>();

        foreach (var market in markets)
        {
            Console.WriteLine($"#{(index + 1):0000}: {market.symbol}"); // {index:D4} // $"{index:0000}" // index.ToString("0###") // index.ToString("D4"); // index.ToString().PadLeft(4, '0')

            entities.Add(new MarketModel(
                symbol: market.symbol,
                quantity_min: Convert.ToDouble(market.quantity_min.Replace(".", ",")),
                quantity_increment: Convert.ToDouble(market.quantity_increment.Replace(".", ",")),
                price_min: Convert.ToDouble(market.price_min.Replace(".", ",")),
                price_increment: Convert.ToDouble(market.price_increment.Replace(".", ",")),
                base_name: market?.@base?.name!,
                base_precision: market?.@base?.precision ?? 0,
                base_symbol: market?.@base?.symbol!,
                base_type: market?.@base?.type!,
                quote_name: market?.quote?.name!,
                quote_precision: market?.quote?.precision ?? 0,
                quote_symbol: market?.quote?.symbol!,
                quote_type: market?.quote?.type!
            ));

            index++;
        }

        await _uow.Markets.AddRangeAsync(entities);
        await _uow.CommitAsync();

        var qtde = await _uow.Markets.GetTotalRegistrosAsync();
        Console.WriteLine("");
        Console.WriteLine($"Markets: {qtde}");
    }
}



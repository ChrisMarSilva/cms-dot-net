using CMS_DotNet_Teste_Resquest_Http_FoxBit.Application.Dtos;
using CMS_DotNet_Teste_Resquest_Http_FoxBit.Application.Services.Interfaces;
using CMS_DotNet_Teste_Resquest_Http_FoxBit.Domain.Models;
using CMS_DotNet_Teste_Resquest_Http_FoxBit.Infrastructure.Context.Interfaces;
using CMS_DotNet_Teste_Resquest_Http_FoxBit.Shared;
using System.Diagnostics.Metrics;
using System.Text.Json;

namespace CMS_DotNet_Teste_Resquest_Http_FoxBit.Application.Services;

public class MarketQuoteService : IMarketQuoteService
{
    private IUnitOfWork _uow;

    public MarketQuoteService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task GetMarketQuotesAsync()
    {
        var markets = await _uow.Markets.GetByWhereAsync(expression: x => x.quote_name.Equals("Real"));

        if (markets == null)
            return;

        await _uow.MarketQuotes.RemoveAllAsync("TbTnBFoxbit_MarketQuotes");

        int index = 0;
        var payloadUrl = "/rest/v3/markets/quotes";
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var entities = new List<MarketQuoteModel>();

        foreach (var market in markets)
        {
            Console.WriteLine($"#{(index + 1):0000}: {market.symbol}");

            var query = new Dictionary<string, string>();
            query["side"] = "buy";
            query["base_currency"] = market.symbol.Remove(market.symbol.Length - 3);
            query["quote_currency"] = "brl";
            query["amount"] = "100";
            var payloadQuery = string.Join("&", query.Select(kv => kv.Key + "=" + kv.Value).ToArray());
            var requestUri = payloadUrl+ "?" + payloadQuery;

            await Task.Delay(millisecondsDelay: 1000 * 2); // 2 Segungos

            var response = "";
            try
            {
                response = await Uteis.GetRequestWithoutAuth(requestUri: requestUri, removeData: false);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERRO: {ex.Message}");
                response = "";
            }

            if (string.IsNullOrEmpty(response))
                continue;

            var marketQuote = JsonSerializer.Deserialize<MarketQuotesResponseDto?>(json: response, options: options);

            if (marketQuote == null)
                continue;

            entities.Add(new MarketQuoteModel(
                side: marketQuote.side,
                market_symbol: marketQuote.market_symbol,
                base_amount: Convert.ToDouble(marketQuote.base_amount.Replace(".", ",")),
                quote_amount: Convert.ToInt16(marketQuote.quote_amount),
                price: Convert.ToDouble(marketQuote.price.Replace(".", ","))
            ));

            index++;
        }

        if (entities.Count > 0)
            await _uow.MarketQuotes.AddRangeAsync(entities);

        await _uow.CommitAsync();

        var qtde = await _uow.MarketQuotes.GetTotalRegistrosAsync();
        Console.WriteLine("");
        Console.WriteLine($"MarketQuotes: {qtde}");
    }
}



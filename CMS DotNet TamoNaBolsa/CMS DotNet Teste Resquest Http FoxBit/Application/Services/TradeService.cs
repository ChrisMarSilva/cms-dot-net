using CMS_DotNet_Teste_Resquest_Http_FoxBit.Application.Dtos;
using CMS_DotNet_Teste_Resquest_Http_FoxBit.Application.Services.Interfaces;
using CMS_DotNet_Teste_Resquest_Http_FoxBit.Domain.Models;
using CMS_DotNet_Teste_Resquest_Http_FoxBit.Infrastructure.Context.Interfaces;
using CMS_DotNet_Teste_Resquest_Http_FoxBit.Shared;
using System.Text.Json;

namespace CMS_DotNet_Teste_Resquest_Http_FoxBit.Application.Services;

public class TradeService : ITradeService
{
    private IUnitOfWork _uow;

    public TradeService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task GetTradesAsync()
    {
        var markets = await _uow.Markets
            .GetByWhereAsync(expression: x => x.quote_name.Equals("Real")); //  && x.symbol.Equals("linkbrl")

        if (markets == null)
            return;

        // await _uow.Trades.RemoveAllAsync("TbTnBFoxbit_Trades");

        int indexMarket = 0;
        var payloadMethod = "GET";
        var payloadUrl = "/rest/v3/trades";
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        foreach (var market in markets)
        {
            Console.WriteLine($"({market.symbol}) ==> #{(indexMarket + 1):0000}");

            //if ((indexMarket + 1) % 5 == 0)
            //    await Task.Delay(millisecondsDelay: 1000 * 1); // 1 Segungos

            await _uow.Trades
                .RemoveWhereAsync(tableName: "TbTnBFoxbit_Trades", where: $"market_symbol = '{market.symbol}'");

            int pageIndex = 1;
            bool hasMorePages = true;
            var entities = new List<TradeModel>();

            while (hasMorePages)
            {
                Console.WriteLine($"({market.symbol}) ======> Index: #{(indexMarket + 1):0000} - Page: {pageIndex}");

                //if ((pageIndex + 1) % 5 == 0)
                //    await Task.Delay(millisecondsDelay: 1000 * 1); // 1 Segungos

                //var query = new Dictionary<string, string>();
                //query["market_symbol"] = market.symbol;

                var query = new Dictionary<string, string>
                {
                    //["start_time"] = "2022-01-01T00:00:00.000Z",
                    //["end_time"] = "2022-02-18T22:06:32.999Z",
                    ["page_size"] = "100",
                    ["page"] = pageIndex.ToString(),
                    ["market_symbol"] = market.symbol
                };

                var payloadQuery = string.Join("&", query.Select(kv => kv.Key + "=" + kv.Value).ToArray());
                var requestUri = payloadUrl + "?" + payloadQuery;
                var timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
                var prehash = $"{timestamp}{payloadMethod}{payloadUrl}{payloadQuery}";
                var signature = Uteis.ToHmacSHA256(prehash, "O2Ga6PXVJlLTrAx454QSq4qXYsCRBB2lW0sXEqM6"); // Chave secreta

                try
                {
                    await Task.Delay(200); // Aguardar 200 milissegundos (5 requisições por segundo)

                    var response = await Uteis
                        .GetRequestWithAuth(requestUri: requestUri, signature: signature, timestamp: timestamp);

                    if (string.IsNullOrEmpty(response))
                    {
                        hasMorePages = false;
                        break;
                    }

                    var trades = JsonSerializer
                        .Deserialize<IEnumerable<TradeResponseDto>?>(json: response, options: options);

                    if (trades == null || !trades.Any())
                    {
                        hasMorePages = false;
                        break;
                    }

                    Console.WriteLine($"({market.symbol}) ======> Index: #{(indexMarket + 1):0000} - Page: {pageIndex} - Qtd: {trades.Count()}");

                    //int indexTrade = 0;
                    foreach (var trade in trades)
                    {
                        //Console.WriteLine($"({market.symbol}) ======> Index: #{(indexTrade + 1):0000} - Page: {pageIndex} - Id: {trade.id} - SN:{trade.sn}");

                        entities.Add(new TradeModel(
                            id: trade.id,
                            sn: trade.sn,
                            order_id: Convert.ToInt64(trade.order_id), // Long.Parse("1100.25") // (long)Convert.ToDouble("1100.25") // Convert.ToInt64(Convert.ToDecimal(strValue))
                            market_symbol: trade.market_symbol,
                            side: trade.side,
                            price: Convert.ToDouble(trade.price.Replace(".", ",")),
                            quantity: Convert.ToDouble(trade.quantity.Replace(".", ",")),
                            fee: Convert.ToDouble(trade.fee.Replace(".", ",")),
                            fee_currency_symbol: trade.fee_currency_symbol,
                            created_at: trade.created_at
                        ));

                        //indexTrade++;
                    } // foreach (var trade in trades)

                    pageIndex++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"({market.symbol}) ======> Index: #{(indexMarket + 1):0000} - Page: {pageIndex} - Erro: {ex.Message}");
                    hasMorePages = false;
                    break;
                }
            } // while (hasMorePages)

            if (entities.Count > 0)
            {
                await _uow.Trades.AddRangeAsync(entities);
                await _uow.CommitAsync();
            }

            indexMarket++;
        } // foreach (var market in markets)

        var qtde = await _uow.Trades.GetTotalRegistrosAsync();
        Console.WriteLine("");
        Console.WriteLine($"Currencies: {qtde}");
    }
}

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
        var payloadMethod = "GET";
        var payloadUrl = "/rest/v3/currencies"; // "/rest/v3/trades";   // markets  // https://statics.foxbit.com.br/icons/colored/btc.svg
        //var query = new Dictionary<string, string>();
        //query["start_time"] = "2023-11-01T22:06:32.999Z";
        //query["end_time"] = "2023-11-30T22:06:32.999Z";
        //query["page_size"] = "100";
        //query["page"] = "1";
        //query["market_symbol"] = "mkrbrl";
        //query["state"] = "ACTIVE";
        //var payloadQuery = string.Join("&", query.Select(kv => kv.Key + "=" + kv.Value).ToArray());
        var requestUri = payloadUrl; // + "?" + payloadQuery;
        var timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
        var prehash = $"{timestamp}{payloadMethod}{payloadUrl}"; // {payloadQuery}";
        var signature = Uteis.ToHmacSHA256(prehash, "O2Ga6PXVJlLTrAx454QSq4qXYsCRBB2lW0sXEqM6"); // Chave secreta

        var response = await Uteis.GetRequestWithAuth(requestUri: requestUri, signature: signature, timestamp: timestamp);

        Console.WriteLine(response);
        Console.WriteLine(Uteis.ToJsonFormat(response));
    }


    public async Task GetCurrenciesAsync()
    {
        var payloadMethod = "GET";
        var payloadUrl = "/rest/v3/currencies";
        var requestUri = payloadUrl;
        var timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
        var prehash = $"{timestamp}{payloadMethod}{payloadUrl}";
        var signature = Uteis.ToHmacSHA256(prehash, "O2Ga6PXVJlLTrAx454QSq4qXYsCRBB2lW0sXEqM6"); // Chave secreta

        var response = await Uteis.GetRequestWithAuth(requestUri: requestUri, signature: signature, timestamp: timestamp);
        if (string.IsNullOrEmpty(response))
            return;

        // var json  = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(response); // string // dynamic
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var currencies = JsonSerializer.Deserialize<IEnumerable<CurrencyResponseDto>?>(json: response, options: options);

        if (currencies == null)
            return;

        await _uow.Currencies.RemoveAllAsync("TbTnBFoxbit_Currencies");

        int index = 0;
        var entities = new List<CurrencyModel>();

        foreach (var currency in currencies)
        {
            Console.WriteLine($"#{(index + 1):0000}: {currency.symbol} - {currency.name}"); // {index:D4} // $"{index:0000}" // index.ToString("0###") // index.ToString("D4"); // index.ToString().PadLeft(4, '0')

            entities.Add(new CurrencyModel(
                symbol: currency.symbol,
                name: currency.name,
                precision: currency.precision,
                category_code: currency?.category?.code ?? "",
                category_name: currency?.category?.name ?? "",
                type: currency!.type,
                deposit_min_to_confirm: Convert.ToDouble(currency?.deposit_info?.min_to_confirm),
                deposit_min_amount: Convert.ToDouble(currency?.deposit_info?.min_amount),
                withdraw_enabled: Convert.ToBoolean(currency?.withdraw_info?.enabled),
                withdraw_min_amount: Convert.ToDouble(currency?.withdraw_info?.min_amount),
                withdraw_fee: Convert.ToDouble(currency?.withdraw_info?.fee)
            ));

            index++;
        }

        await _uow.Currencies.AddRangeAsync(entities);
        await _uow.CommitAsync();

        var qtde = await _uow.Currencies.GetTotalRegistrosAsync();
        Console.WriteLine("");
        Console.WriteLine($"Currencies: {qtde}");
    }


    //public async Task<IEnumerable<TradeModel>?> GetAllAsync()
    //{
    //    try
    //    {
    //        var results = await _uow.Trades.GetAllAsync();

    //        return results;
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}

    //public async Task<TradeModel?> GetByIdAsync(int id)
    //{
    //    try
    //    {
    //        var result = await _uow.Trades.GetByIdNoTrackingAsync(p => p.id == id);

    //        if (result == null)
    //            return null; // new TradeModel();

    //        return result;
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}

    //public async Task<TradeModel?> InsertAsync(TradeModel input)
    //{
    //    try
    //    {
    //        var trade = new TradeModel();

    //        var result = await _uow.Trades.AddAsync(trade);

    //        if (result is null)
    //            return null; // new TradeModel();

    //        var resultCommit = await _uow.CommitAsync();

    //        if (!resultCommit)
    //            throw new Exception("Erro ao commitar inclusção"); // return null; // new TradeModel();

    //        return result;
    //    }
    //    catch 
    //    {
    //        throw;
    //    }
    //}

    //public async Task<TradeModel?> UpdateAsync(int id, TradeModel input)
    //{
    //    try
    //    {
    //        var trade = await _uow.Trades.GetByIdAsync(p => p.id == id);

    //        if (trade == null)
    //            return null; // new TradeModel();

    //        //trade.Update(
    //        //    nome: input.Nome,
    //        //    email: input.Email,
    //        //    idade: input.Idade
    //        //);

    //        var result = _uow.Trades.Update(trade);

    //        if (result is null)
    //            return null; // new TradeModel();

    //        var resultCommit = await _uow.CommitAsync();

    //        if (!resultCommit)
    //            throw new Exception("Erro ao commitar alteração"); // return null; // new TradeModel();


    //        return result;
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}

    //public async Task<bool> DeleteAsync(int id)
    //{
    //    try
    //    {
    //        var result = await _uow.Trades.GetByIdAsync(p => p.id == id);

    //        if (result is null)
    //            return false;

    //        var status = _uow.Trades.Remove(result);

    //        if (!status)
    //            return false;

    //        var resultCommit = await _uow.CommitAsync();

    //        if (!resultCommit)
    //            throw new Exception("Erro ao commitar exclusão"); 

    //        return true;
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}
}

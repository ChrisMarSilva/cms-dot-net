using CMS_DotNet_Teste_Resquest_Http_FoxBit.Application.Dtos;
using CMS_DotNet_Teste_Resquest_Http_FoxBit.Application.Services.Interfaces;
using CMS_DotNet_Teste_Resquest_Http_FoxBit.Domain.Models;
using CMS_DotNet_Teste_Resquest_Http_FoxBit.Infrastructure.Context.Interfaces;
using CMS_DotNet_Teste_Resquest_Http_FoxBit.Shared;
using Microsoft.Extensions.Logging;

namespace CMS_DotNet_Teste_Resquest_Http_FoxBit.Application.Services;

public class TradeService : ITradeService
{
    private readonly ILogger<TradeService> _logger;
    private IUnitOfWork _uow;

    public TradeService(ILogger<TradeService> logger, IUnitOfWork uow)
    {
        _logger = logger;
        _uow = uow;
    }

    public async Task Teste() 
    {
        await Task.Delay(1);

        // https://docs.foxbit.com.br/rest/v3/#tag/Deposit/operation/DepositsController_listOrders

        Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("pt-BR");
        Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("pt-BR");

        var Idempotent = Guid.NewGuid();
        var apiSecret = "O2Ga6PXVJlLTrAx454QSq4qXYsCRBB2lW0sXEqM6"; // Chave secreta
        var payloadMethod = "GET";
        var payloadUrl = "/rest/v3/trades"; // todos/1 // currencies // markets // banks // me // orders // trades
        var query = new Dictionary<string, string>(); //  HttpUtility.ParseQueryString(string.Empty, Encoding.UTF8);
        query["start_time"] = "2023-11-01T22:06:32.999Z";
        query["end_time"] = "2023-11-30T22:06:32.999Z";
        query["page_size"] = "100";
        query["page"] = "1";
        query["market_symbol"] = "mkrbrl";
        query["state"] = "ACTIVE";
        var queryString = string.Join("&", query.Select(kv => kv.Key + "=" + kv.Value).ToArray()); // string.Join("&", query); // query.ToString();
        var payloadQuery = queryString;

        var timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds(); // DateTime.Now.ToString("yyyyMMddHHmmssffff");
        var prehash = $"{timestamp}{payloadMethod}{payloadUrl}{payloadQuery}";
        var signature = Uteis.HmacSHA256(prehash, apiSecret); // CalcHMACSHA256Hash // HmacSHA256
        //Console.WriteLine($"signature: {signature.ToString()}");

        //ID do usuário: 258442
        //Chave de acesso: Ksh9WoRMI3xVdAklxI6mbfwZiWyGeIN6zSSGOmr7
        //Chave secreta: O2Ga6PXVJlLTrAx454QSq4qXYsCRBB2lW0sXEqM6

        using var client = new HttpClient();

        client.BaseAddress = new Uri("https://api.foxbit.com.br"); // https://jsonplaceholder.typicode.com/
        client.DefaultRequestHeaders.Accept.Clear();
        //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //client.DefaultRequestHeaders.Add("Content-Type", "application/json;charset=utf-8");
        //client.DefaultRequestHeaders.Add("Content-Language", "pt-BR");
        //client.DefaultRequestHeaders.Add("Accept", "application/json");
        //client.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
        //client.DefaultRequestHeaders.Add("pragma", "no-cache");
        //client.DefaultRequestHeaders.Add("cache-control", "no-cache");
        //client.DefaultRequestHeaders.Add("accept", "*/*");
        //client.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.183 Safari/537.36");
        //client.DefaultRequestHeaders.Add("ccess-Control-Allow-Origi", "*");
        //client.DefaultRequestHeaders.Add("Access-Control-Allow-Credentials", "true");
        //client.DefaultRequestHeaders.Add("Access-Control-Allow-Methods", "GET, POST, PATCH, PUT, DELETE, OPTIONS");
        //client.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Origin, Content-Type, Accept, Authorization, X-Auth-Token");
        //client.DefaultRequestHeaders.Add("X-Version","1");
        client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
        client.DefaultRequestHeaders.TryAddWithoutValidation("X-FB-ACCESS-KEY", "Ksh9WoRMI3xVdAklxI6mbfwZiWyGeIN6zSSGOmr7"); // Chave de acesso
        client.DefaultRequestHeaders.TryAddWithoutValidation("X-FB-ACCESS-TIMESTAMP", timestamp.ToString());
        client.DefaultRequestHeaders.TryAddWithoutValidation("X-FB-ACCESS-SIGNATURE", signature.ToString());
        client.DefaultRequestHeaders.TryAddWithoutValidation("X-Idempotent", Idempotent.ToString());

        using var response = client.GetAsync(payloadUrl + "?" + payloadQuery).Result;
        response.EnsureSuccessStatusCode();
        //Console.WriteLine(response.Headers.ToString());
        //Console.WriteLine(response.RequestMessage);

        if (response.IsSuccessStatusCode)
        {
            var responseBody = response.Content.ReadAsStringAsync().Result; // responseBody // responseData
            Console.WriteLine(responseBody);
        }

    }

    public async Task<IEnumerable<TradeModel>> GetAllAsync()
    {
        try
        {
            var results = await _uow.Trades.GetAllAsync();

            return results;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<TradeModel> GetByIdAsync(int id)
    {
        try
        {
            var result = await _uow.Trades.GetByIdNoTrackingAsync(p => p.id == id);

            if (result == null)
                return null; // new TradeModel();

            return result;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<TradeModel> InsertAsync(TradeModel input)
    {
        try
        {
            var trade = new TradeModel();

            var result = await _uow.Trades.AddAsync(trade);

            if (result is null)
                return null; // new TradeModel();

            var resultCommit = await _uow.CommitAsync();

            if (!resultCommit)
                throw new Exception("Erro ao commitar inclusção"); // return null; // new TradeModel();

            return result;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<TradeModel> UpdateAsync(int id, TradeModel input)
    {
        try
        {
            var trade = await _uow.Trades.GetByIdAsync(p => p.id == id);

            if (trade == null)
                return null; // new TradeModel();

            //trade.Update(
            //    nome: input.Nome,
            //    email: input.Email,
            //    idade: input.Idade
            //);

            var result = _uow.Trades.Update(trade);

            if (result is null)
                return null; // new TradeModel();

            var resultCommit = await _uow.CommitAsync();

            if (!resultCommit)
                throw new Exception("Erro ao commitar alteração"); // return null; // new TradeModel();


            return result;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var result = await _uow.Trades.GetByIdAsync(p => p.id == id);

            if (result is null)
                return false;

            var status = _uow.Trades.Remove(result);

            if (!status)
                return false;

            var resultCommit = await _uow.CommitAsync();

            if (!resultCommit)
                throw new Exception("Erro ao commitar exclusão"); 

            return true;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}

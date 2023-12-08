using CMS_DotNet_Teste_Resquest_Http_FoxBit.Application.Dtos;
using CMS_DotNet_Teste_Resquest_Http_FoxBit.Application.Services.Interfaces;
using CMS_DotNet_Teste_Resquest_Http_FoxBit.Domain.Models;
using CMS_DotNet_Teste_Resquest_Http_FoxBit.Infrastructure.Context.Interfaces;
using CMS_DotNet_Teste_Resquest_Http_FoxBit.Shared;
using System.Text.Json;

namespace CMS_DotNet_Teste_Resquest_Http_FoxBit.Application.Services;

public class SystemTimeService : ISystemTimeService
{
    private IUnitOfWork _uow;

    public SystemTimeService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task GetTimeAsync(string type)
    {
        var requestUri = "/rest/v3/system/time";
        var response = await Uteis.GetRequestWithoutAuth(requestUri: requestUri, removeData: false);

        if (string.IsNullOrEmpty(response))
            return;

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var time = JsonSerializer.Deserialize<SystemTimeResponseDto?>(json: response, options: options);

        if (time == null)
            return;

        Console.WriteLine($"{type} - {time.iso} - {time.timestamp}");

        await _uow.SystemTimes.RemoveAllAsync("TbTnBFoxbit_SystemTime");

        var entite = new SystemTimeModel(
            type: type,
            iso: Convert.ToDateTime(time.iso), // DateTime.Parse(time.iso) // DateTime.ParseExact(time.iso, "yyyy-MM-ddTHH:mm:ss,fff", System.Globalization.CultureInfo.InvariantCulture); //DateTime.Parse(BrazilianDate, new CultureInfo("pt-BR")); DateTime.ParseExact(BrazilianDate, "yyyy-MM-dd HH:mm:ss.fff", new CultureInfo("pt-BR"));
            timestamp: time.timestamp);

        await _uow.SystemTimes.AddAsync(entite);
        await _uow.CommitAsync();

        var qtde = await _uow.SystemTimes.GetTotalRegistrosAsync();
        Console.WriteLine("");
        Console.WriteLine($"SystemTimes: {qtde}");
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

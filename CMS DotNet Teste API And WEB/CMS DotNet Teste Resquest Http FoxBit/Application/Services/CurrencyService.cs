using CMS_DotNet_Teste_Resquest_Http_FoxBit.Application.Dtos;
using CMS_DotNet_Teste_Resquest_Http_FoxBit.Application.Services.Interfaces;
using CMS_DotNet_Teste_Resquest_Http_FoxBit.Domain.Models;
using CMS_DotNet_Teste_Resquest_Http_FoxBit.Infrastructure.Context.Interfaces;
using CMS_DotNet_Teste_Resquest_Http_FoxBit.Shared;
using System.Text.Json;

namespace CMS_DotNet_Teste_Resquest_Http_FoxBit.Application.Services;

public class CurrencyService : ICurrencyService
{
    private IUnitOfWork _uow;

    public CurrencyService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task GetCurrenciesAsync()
    {
        var requestUri = "/rest/v3/currencies";
        var response = await Uteis.GetRequestWithoutAuth(requestUri: requestUri);

        if (string.IsNullOrEmpty(response))
            return;

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var currencies = JsonSerializer.Deserialize<IEnumerable<CurrencyResponseDto>?>(json: response, options: options);

        if (currencies == null)
            return;

        await _uow.Currencies.RemoveAllAsync("TbTnBFoxbit_Currencies");

        int index = 0;
        var entities = new List<CurrencyModel>();

        foreach (var currency in currencies)
        {
            Console.WriteLine($"#{(index+1):0000}: {currency.symbol} - {currency.name}");

            entities.Add(new CurrencyModel(
                symbol: currency.symbol,
                name: currency.name,
                precision: currency.precision,
                category_code: currency?.category?.code ?? "",
                category_name: currency?.category?.name ?? "",
                type: currency!.type,
                deposit_min_to_confirm: Convert.ToDouble(currency?.deposit_info?.min_to_confirm?.Replace(".", ",")),
                deposit_min_amount: Convert.ToDouble(currency?.deposit_info?.min_amount?.Replace(".", ",")),
                withdraw_enabled: Convert.ToBoolean(currency?.withdraw_info?.enabled),
                withdraw_min_amount: Convert.ToDouble(currency?.withdraw_info?.min_amount?.Replace(".", ",")),
                withdraw_fee: Convert.ToDouble(currency?.withdraw_info?.fee?.Replace(".", ","))
            ));

            index++;
        }

        await _uow.Currencies.AddRangeAsync(entities);
        await _uow.CommitAsync();

        var qtde = await _uow.Currencies.GetTotalRegistrosAsync();
        Console.WriteLine("");
        Console.WriteLine($"Currencies: {qtde}");
    }

    public async Task GetIconCurrenciesAsync()
    {
        var currencies = await _uow.Currencies.GetByWhereAsync(expression: x => x.type.Equals("CRYPTO"));

        if (currencies == null)
            return;

        int index = 0;
        var path = @"C:\Users\chris\Desktop\CMS DotNet\CMS DotNet Teste API And WEB\CMS DotNet Teste Resquest Http FoxBit\Icons\";
        using var httpClient = new HttpClient() { Timeout = Timeout.InfiniteTimeSpan };

        foreach (var currency in currencies)
        {
            Console.WriteLine($"#{(index + 1):0000}: {currency.symbol}");

            try
            {
                //var httpResult = await httpClient.GetAsync(requestUri: $"https://statics.foxbit.com.br/icons/colored/{currency.symbol}.svg");
                using (var httpResult = await httpClient.GetStreamAsync(requestUri: $"https://statics.foxbit.com.br/icons/colored/{currency.symbol}.svg"))
                {

                    //httpResult.EnsureSuccessStatusCode();
                    //if (!httpResult.IsSuccessStatusCode)
                    //    throw new ApplicationException($"Something went wrong calling the API: {httpResult.ReasonPhrase}");

                    //using (var resultStream = await httpResult.Content.ReadAsStreamAsync())
                    //{
                    using (var fileStream = File.Create(path: path + $"{currency.symbol}.svg"))
                    {
                        await httpResult.CopyToAsync(fileStream);
                        //await resultStream.CopyToAsync(fileStream);
                        // await resultStream.Content.CopyToAsync(fs);
                    }
                    //}
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERRO: {ex.Message}");
                continue;
            }

            index++;
        }

        Console.WriteLine("");
        Console.WriteLine($"Icons: {index+1}");
    }
}



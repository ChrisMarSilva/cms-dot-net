namespace CMS_DotNet_Teste_Resquest_Http_FoxBit.Application.Services.Interfaces;

public interface ICurrencyService
{
    Task GetCurrenciesAsync();
    Task GetIconCurrenciesAsync();
}

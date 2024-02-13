namespace CMS_DotNet_Teste_Resquest_Http_FoxBit.Application.Services.Interfaces;

public interface ISystemTimeService
{
    Task GetTimeAsync(string type);
}
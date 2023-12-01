using CMS_DotNet_Teste_Resquest_Http_FoxBit.Controllers;
using CMS_DotNet_Teste_Resquest_Http_FoxBit.Domain.Models;
using CMS_DotNet_Teste_Resquest_Http_FoxBit.Infrastructure.Context;

Console.WriteLine("INI");
try
{
    var tradeController = new TradeController();
    tradeController.GetAll();

    using var context = new AppDbContext();

    //var trade1 = context.Trades.AsNoTracking().FirstOrDefault(x => x.id == 1);

    var trade = new TradeModel();
    context.Trades.Add(trade);

    context.SaveChanges();


}
catch (Exception ex)
{
    Console.WriteLine($"ERRO: {ex.Message}");
}
finally
{
    Console.WriteLine("FIM");
    Console.ReadKey();
}

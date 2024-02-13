using CMS_DotNet_Teste_Resquest_Http_FoxBit.Application.Services;
using CMS_DotNet_Teste_Resquest_Http_FoxBit.Application.Services.Interfaces;
using CMS_DotNet_Teste_Resquest_Http_FoxBit.Infrastructure.Context;
using CMS_DotNet_Teste_Resquest_Http_FoxBit.Infrastructure.Context.Interfaces;
using CMS_DotNet_Teste_Resquest_Http_FoxBit.Infrastructure.Repositories;
using CMS_DotNet_Teste_Resquest_Http_FoxBit.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

//ID do usuário: 258442
//Chave de acesso: Ksh9WoRMI3xVdAklxI6mbfwZiWyGeIN6zSSGOmr7
//Chave secreta: O2Ga6PXVJlLTrAx454QSq4qXYsCRBB2lW0sXEqM6

Console.WriteLine("INI");
try
{
    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("pt-BR");
    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("pt-BR");

    var serviceCollection = new ServiceCollection();

    serviceCollection.AddScoped<ISystemTimeService, SystemTimeService>();
    serviceCollection.AddScoped<ICurrencyService, CurrencyService>();
    serviceCollection.AddScoped<IMarketService, MarketService>();
    serviceCollection.AddScoped<IMarketQuoteService, MarketQuoteService>();
    serviceCollection.AddScoped<IMemberInfoService, MemberInfoService>();
    serviceCollection.AddScoped<ITradeService, TradeService>();

    serviceCollection.AddScoped<ISystemTimeRepository, SystemTimeRepository>();
    serviceCollection.AddScoped<ICurrencyRepository, CurrencyRepository>();
    serviceCollection.AddScoped<IMarketRepository, MarketRepository>();
    serviceCollection.AddScoped<IMarketQuoteRepository, MarketQuoteRepository>();
    serviceCollection.AddScoped<IMemberInfoRepository, MemberInfoRepository>();
    serviceCollection.AddScoped<ITradeRepository, TradeRepository>();

    serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
    var connectionString = "Server=localhost;Port=3306;Database=tamo_na_bolsa_foxbit;Uid=root;Pwd=Chrs8723;Persist Security Info=False;Connect Timeout=300;Connection Reset=False;Max Pool Size=300;";
    serviceCollection.AddDbContextPool<AppDbContext>(opt => opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

    var serviceProvider = serviceCollection.BuildServiceProvider();

    //SystemTime
    //var eventSystemTimeService = serviceProvider.GetService<ISystemTimeService>();
    //await eventSystemTimeService!.GetTimeAsync("Trade");

    //Currency
    //var eventCurrencyService = serviceProvider.GetService<ICurrencyService>();
    //await eventCurrencyService!.GetCurrenciesAsync();
    //await eventCurrencyService!.GetIconCurrenciesAsync();

    //Market
    //var eventMarketService = serviceProvider.GetService<IMarketService>();
    //await eventMarketService!.GetMarketsAsync();

    //MarketQuote
    //var eventMarketQuoteService = serviceProvider.GetService<IMarketQuoteService>();
    //await eventMarketQuoteService!.GetMarketQuotesAsync();

    //var eventMemberInfoService = serviceProvider.GetService<IMemberInfoService>();
    //await eventMemberInfoService!.GetInfosAsync();

    var eventTradeService = serviceProvider.GetService<ITradeService>();
    await eventTradeService!.GetTradesAsync();

    //var trades = eventService?.GetAllAsync().Result;
    //Console.WriteLine(trades);

    //var tradeController = new TradeController();
    //tradeController.GetAll();

    //var builder = new DbContextOptionsBuilder<AppDbContext>();
    //using var context = new AppDbContext(builder.Options);
    //using var context = new AppDbContext();
    //var trades = context.Trades.Where(x => x.id > 0).ToList();
    //Console.WriteLine(trades);
    //var trade = new TradeModel(id: 1, sn: "sss", client_order_id: 2, market_symbol: "sss", side: "sss", type: "sss", state: "sss", price: 3, price_avg: 3, quantity: 3, quantity_executed: 3, instant_amount: 3, instant_amount_executed: 3, created_at: DateTime.Now, trades_count: 3, remark: "sss", funds_received: 3);
    //context.Trades.Add(trade);
    //context.SaveChanges();
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

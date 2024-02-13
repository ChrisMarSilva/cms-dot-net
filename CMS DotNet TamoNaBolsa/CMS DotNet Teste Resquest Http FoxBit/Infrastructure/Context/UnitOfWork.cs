using CMS_DotNet_Teste_Resquest_Http_FoxBit.Infrastructure.Context.Interfaces;
using CMS_DotNet_Teste_Resquest_Http_FoxBit.Infrastructure.Repositories.Interfaces;

namespace CMS_DotNet_Teste_Resquest_Http_FoxBit.Infrastructure.Context;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly AppDbContext _ctx;
    private bool _disposed;

    public ISystemTimeRepository SystemTimes { get; private set; }
    public ICurrencyRepository Currencies { get; private set; }
    public IMarketRepository Markets { get; private set; }
    public IMarketQuoteRepository MarketQuotes { get; private set; }
    public IMemberInfoRepository MemberInfos { get; private set; }
    public ITradeRepository Trades { get; private set; }

    public UnitOfWork(
        AppDbContext ctx,
        ISystemTimeRepository systemTimeRepo,
        ICurrencyRepository currenciesRepo,
        IMarketRepository marketsRepo,
        IMarketQuoteRepository marketQuotesRepo,
        IMemberInfoRepository memberInfoRepo,
        ITradeRepository tradesRepo)
    {
        _ctx = ctx;
        SystemTimes = systemTimeRepo ?? throw new ArgumentNullException(nameof(ISystemTimeRepository));
        Currencies = currenciesRepo ?? throw new ArgumentNullException(nameof(ICurrencyRepository));
        Markets = marketsRepo ?? throw new ArgumentNullException(nameof(IMarketRepository));
        MarketQuotes = marketQuotesRepo ?? throw new ArgumentNullException(nameof(IMarketQuoteRepository));
        MemberInfos = memberInfoRepo ?? throw new ArgumentNullException(nameof(IMemberInfoRepository));
        Trades = tradesRepo ?? throw new ArgumentNullException(nameof(ITradeRepository));
    }

    public void Commit() => _ctx.SaveChanges();
    public void Rollback() { }
    public async Task<bool> CommitAsync() => await _ctx.SaveChangesAsync() > 0;
    //public async Task<bool> RollbackAsync() => true;

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
            _ctx.Dispose();
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}

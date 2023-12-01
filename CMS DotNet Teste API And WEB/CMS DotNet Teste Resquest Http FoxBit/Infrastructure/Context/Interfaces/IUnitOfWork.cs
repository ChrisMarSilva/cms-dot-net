using CMS_DotNet_Teste_Resquest_Http_FoxBit.Infrastructure.Repositories.Interfaces;

namespace CMS_DotNet_Teste_Resquest_Http_FoxBit.Infrastructure.Context.Interfaces;

public interface IUnitOfWork
{
    ITradeRepository Trades { get; }
    void Commit();
    void Rollback();
    Task<bool> CommitAsync();
    Task<bool> RollbackAsync();
}

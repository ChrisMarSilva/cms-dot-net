using CMS_DotNet_Teste_Resquest_Http_FoxBit.Domain.Models;
using CMS_DotNet_Teste_Resquest_Http_FoxBit.Infrastructure.Context;
using CMS_DotNet_Teste_Resquest_Http_FoxBit.Infrastructure.Repositories.Interfaces;

namespace CMS_DotNet_Teste_Resquest_Http_FoxBit.Infrastructure.Repositories;

public class CurrencyRepository : BaseRepository<CurrencyModel>, ICurrencyRepository
{
    public CurrencyRepository(AppDbContext ctx) : base(ctx) { }
}

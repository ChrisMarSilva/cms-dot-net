using CMS_DotNet_Teste_Resquest_Http_FoxBit.Domain.Models;
using CMS_DotNet_Teste_Resquest_Http_FoxBit.Infrastructure.Context;
using CMS_DotNet_Teste_Resquest_Http_FoxBit.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CMS_DotNet_Teste_Resquest_Http_FoxBit.Infrastructure.Repositories;

public class TradeRepository : BaseRepository<TradeModel>, ITradeRepository
{
    public TradeRepository(AppDbContext ctx) : base(ctx) { }

    public async Task<IEnumerable<TradeModel>> GetAllAsync()
    {
        return await base.GetAll()
            //.Where(c => c.DataCadastro >= new DateTime(2000, 1, 1))
            //.OrderBy(c => c.DataCadastro)
            .ToListAsync()
            .ConfigureAwait(false);
    }
}

using CMS_DotNet_Teste_Resquest_Http_FoxBit.Domain.Models;
using CMS_DotNet_Teste_Resquest_Http_FoxBit.Infrastructure.Context;
using CMS_DotNet_Teste_Resquest_Http_FoxBit.Infrastructure.Repositories.Interfaces;

namespace CMS_DotNet_Teste_Resquest_Http_FoxBit.Infrastructure.Repositories;

public class MemberInfoRepository : BaseRepository<MemberInfoModel>, IMemberInfoRepository
{
    public MemberInfoRepository(AppDbContext ctx) : base(ctx) { }
}


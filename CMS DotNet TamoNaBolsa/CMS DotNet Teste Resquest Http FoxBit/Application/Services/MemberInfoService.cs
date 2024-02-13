using CMS_DotNet_Teste_Resquest_Http_FoxBit.Application.Dtos;
using CMS_DotNet_Teste_Resquest_Http_FoxBit.Application.Services.Interfaces;
using CMS_DotNet_Teste_Resquest_Http_FoxBit.Domain.Models;
using CMS_DotNet_Teste_Resquest_Http_FoxBit.Infrastructure.Context.Interfaces;
using CMS_DotNet_Teste_Resquest_Http_FoxBit.Shared;
using System.Text.Json;

namespace CMS_DotNet_Teste_Resquest_Http_FoxBit.Application.Services;

public class MemberInfoService : IMemberInfoService
{
    private IUnitOfWork _uow;

    public MemberInfoService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task GetInfosAsync()
    {
        var payloadMethod = "GET";
        var payloadUrl = "/rest/v3/me";
        var requestUri = payloadUrl;
        var timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
        var prehash = $"{timestamp}{payloadMethod}{payloadUrl}";
        var signature = Uteis.ToHmacSHA256(prehash, "O2Ga6PXVJlLTrAx454QSq4qXYsCRBB2lW0sXEqM6"); // Chave secreta

        var response = await Uteis.GetRequestWithAuth(requestUri: requestUri, signature: signature, timestamp: timestamp, removeData: false);

        if (string.IsNullOrEmpty(response))
            return;

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var memberInfo = JsonSerializer.Deserialize<MemberInfoResponseDto?>(json: response, options: options);

        if (memberInfo == null)
            return;

        Console.WriteLine($"{memberInfo.sn} - {memberInfo.email} - {memberInfo.created_at}");
        Console.WriteLine(memberInfo);

        await _uow.MemberInfos.RemoveAllAsync("TbTnBFoxbit_MemberInfos");

        var entite = new MemberInfoModel(
            sn: memberInfo.sn,
            email: memberInfo.email,
            level: memberInfo.level,
            created_at: memberInfo.created_at,
            disabled: memberInfo.disabled);

        await _uow.MemberInfos.AddAsync(entite);
        await _uow.CommitAsync();

        var qtde = await _uow.MemberInfos.GetTotalRegistrosAsync();
        Console.WriteLine("");
        Console.WriteLine($"MemberInfo: {qtde}");
    }
}


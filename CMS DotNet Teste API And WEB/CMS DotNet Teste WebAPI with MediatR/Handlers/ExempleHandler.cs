using CMS_DotNet_Teste_WebAPI_with_MediatR.Requests;
using CMS_DotNet_Teste_WebAPI_with_MediatR.Services;
using MediatR;

namespace CMS_DotNet_Teste_WebAPI_with_MediatR.Handlers;

public class ExempleHandler : IRequestHandler<ExempleRequest, IResult>
{
    private readonly GuidService _guidService;

    public ExempleHandler(GuidService guidService)
    {
        _guidService = guidService;
    }

    public async Task<IResult> Handle(ExempleRequest request, CancellationToken cancellationToken)
    {
        await Task.Delay(10, cancellationToken);
        var result = new { 
            message = $"The age was: {request.Age} and the name was: {request.Name}",
            resquestGuid = request.GuidService.Id,
            ctorGuid = _guidService.Id,
        };
        return Results.Ok(result);
    }
}

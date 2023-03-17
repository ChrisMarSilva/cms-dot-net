using CMS_DotNet_Teste_WebAPI_with_MediatR.Requests;
using CMS_DotNet_Teste_WebAPI_with_MediatR.Services;
using MediatR;

namespace CMS_DotNet_Teste_WebAPI_with_MediatR.Handlers;

public class WeatherHandler : IRequestHandler<WeatherRequest, IResult>
{
    private readonly WeatherService _weatherService;

    public WeatherHandler(WeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    public async Task<IResult> Handle(WeatherRequest request, CancellationToken cancellationToken)
    {
        var result = await _weatherService.GetWeatherForCityAsync(request.City, request.Days);
        return Results.Ok(result);
    }
}

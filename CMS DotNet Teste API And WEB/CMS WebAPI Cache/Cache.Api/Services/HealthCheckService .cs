using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Cache.Api.Services;

public class HealthCheckService : IHealthCheck
{
    private readonly HttpClient _httpClient;

    public HealthCheckService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var url = "https://jsonplaceholder.typicode.com/todos/1";
        var response = await _httpClient.GetAsync(url, cancellationToken);

        return response.IsSuccessStatusCode
            ? HealthCheckResult.Healthy("Serviço está online.")
            : HealthCheckResult.Unhealthy("Serviço está offline.");

        //return HealthCheckResult.Healthy("Serviço está online.");
    }
}
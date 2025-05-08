using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Cache.App.Api.Services;

public class HealthCheckService : IHealthCheck
{
    private readonly HttpClient _httpClient;

    public HealthCheckService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _httpClient.GetAsync(
                requestUri: "https://jsonplaceholder.typicode.com/todos/1",
                cancellationToken: cancellationToken);

            return response.IsSuccessStatusCode
                ? HealthCheckResult.Healthy("Serviço está online.")
                : HealthCheckResult.Degraded("Serviço está offline.");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy(ex.Message);
        }
    }
}
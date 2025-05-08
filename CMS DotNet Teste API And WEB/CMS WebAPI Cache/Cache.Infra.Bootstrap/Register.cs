using Cache.App.Api;
using Cache.App.Worker;
using Cache.Infra.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cache.Infra.Bootstrap;

public static class Register
{
    private static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDataContext(configuration);

        return services;
    }

    public static IServiceCollection AddServicesForApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfra(configuration);
        services.AddDefaultHealthChecks(configuration);
        services.AddAppServicesForApi(configuration);

        return services;
    }

    public static IServiceCollection AddServicesForAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfra(configuration);
        services.AddDefaultHealthChecks(configuration);

        return services;
    }

    public static IServiceCollection AddServicesForWorker(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfra(configuration);
        services.AddAppServicesForWorker(configuration);

        return services;
    }
}
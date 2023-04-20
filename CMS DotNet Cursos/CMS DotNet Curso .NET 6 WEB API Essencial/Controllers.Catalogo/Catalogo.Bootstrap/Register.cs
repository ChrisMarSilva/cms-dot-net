using Catalogo.Application;
using Catalogo.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalogo.Bootstrap; // CrossCutting

public static class Register
{
    public static IServiceCollection AddServicesForApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);
        services.AddApplication(configuration);

        return services;
    }
}

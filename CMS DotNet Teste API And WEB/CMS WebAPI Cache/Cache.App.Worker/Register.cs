using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cache.App.Worker;

public static class Register
{
    public static IServiceCollection AddAppServicesForWorker(this IServiceCollection services, IConfiguration configuration)
    {


        return services;
    }
}
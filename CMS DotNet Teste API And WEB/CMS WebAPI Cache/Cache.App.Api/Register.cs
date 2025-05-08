using Cache.App.Api.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cache.App.Api;

public static class Register
{
    public static IServiceCollection AddAppServicesForApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddSingleton<ICacheService, RedisCacheService>();

        // services.AddValidatorsFromAssembly(typeof(Program).Assembly, includeInternalTypes: true);
        // services.AddValidatorsFromAssemblyContaining<UserRequestDtoValidator>();
        // services.AddScoped<IValidator<UserRequestDto>, UserRequestDtoValidator>();
        // services.Configure<ValidationSettings>(configuration.GetSection("ValidationSettings"));

        return services;
    }
}

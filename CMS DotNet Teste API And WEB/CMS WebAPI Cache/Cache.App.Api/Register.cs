using Cache.App.Api.Services;
using Cache.Domain.Models.Auth;
using Microsoft.AspNetCore.Identity;
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

    public static IServiceCollection AddAppServicesForAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthorization();
        services.AddAuthentication().AddCookie(IdentityConstants.ApplicationScheme);

        services
            .AddIdentityCore<ApplicationUser>();
            //.AddEntityFrameworkStores<AuthDbContext>()
            //.AddApiEndpoints();

        /*
         
         builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "geek_shopping");
    });
});

         */
        return services;
    }
}

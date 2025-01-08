using Cache.Api.Database.Contexts;
using Cache.Api.Repositories;
using Cache.Api.Services;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.IO.Compression;

namespace Cache.Api.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddCompression(this IServiceCollection services)
    {
        services.AddResponseCompression(options =>
        {
            options.EnableForHttps = true;
            options.Providers.Add<BrotliCompressionProvider>();
            options.Providers.Add<GzipCompressionProvider>();
        }).Configure<GzipCompressionProviderOptions>(options => { options.Level = CompressionLevel.Fastest; }) // SmallestSize
          .Configure<BrotliCompressionProviderOptions>(options => { options.Level = CompressionLevel.Fastest; });

        return services;
    }

    public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
    {
        //var connectionStringsConfiguration = configuration.GetSection("ConnectionStrings");
        //var connectionString = connectionStringsConfiguration.GetValue<string>("DefaultConnection");
        //var connectionString = configuration.GetConnectionString("DefaultConnectionSQLServer");
        //var connectionString = configuration.GetConnectionString("DefaultConnectionMySQL");
        //var connectionString = configuration.GetConnectionString("DefaultConnectionSQLite");
        var connectionString = configuration.GetConnectionString("DefaultConnectionPostgres");

        services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(connectionString));
        //services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(connectionString, builder => { builder.CommandTimeout(30); })}).AddScoped<IDataContext>(sp => sp.GetRequiredService<AppWriteDbContext>());
        //services.AddDbContext<AppDbContext>(opt => opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
        //services.AddDbContext<AppDbContext>(opt => opt.UseSqlite(connectionString));
        //services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("TarefasDB"));

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddTransient<IUserRepository, UserRepository>();

        //services.AddMemoryCache();
        // services.AddSingleton<ICacheService, InMemoryCacheService>();

        var connectionStringRedis = configuration.GetValue("ConnectionStrings:Redis", "localhost:6379");
        //services.AddStackExchangeRedisCache(opt => { opt.Configuration = connectionStringRedis; opt.InstanceName = "RedisDemo_"; });
        //services.AddScoped<IConnectionMultiplexer>(cfg => ConnectionMultiplexer.Connect(connectionStringRedis)});
        services.AddSingleton<IDatabase>(cfg => ConnectionMultiplexer.Connect(connectionStringRedis).GetDatabase());
        services.AddSingleton<ICacheService, RedisCacheService>();

        services.AddLogging();

        return services;
    }


    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();

        services.AddValidatorsFromAssembly(typeof(Program).Assembly, includeInternalTypes: true);
        // services.AddValidatorsFromAssemblyContaining<UserRequestDtoValidator>();
        // services.AddScoped<IValidator<UserRequestDto>, UserRequestDtoValidator>();
        // services.Configure<ValidationSettings>(configuration.GetSection("ValidationSettings"));

        return services;
    }

    public static IApplicationBuilder UseAppCors(this IApplicationBuilder app)
    {
        // return app.UseCors("CorsPolicy");

        app.UseCors(p => {
            p.AllowAnyOrigin();
            p.WithMethods(); // "GET"
            p.AllowAnyHeader();
        });

        return app;
    }

    public static IDictionary<string, string[]> ToDictionary(this ValidationResult validationResult)
    {
        return validationResult.Errors
          .GroupBy(x => x.PropertyName)
          .ToDictionary(
            g => g.Key,
            g => g.Select(x => x.ErrorMessage).ToArray()
          );
    }

    //public static void AddToModelState(this ValidationResult result, ModelStateDictionary modelState)
    //{
    //    foreach (var error in result.Errors)
    //    {
    //        modelState.AddModelError(error.PropertyName, error.ErrorMessage);
    //    }
    //}
}
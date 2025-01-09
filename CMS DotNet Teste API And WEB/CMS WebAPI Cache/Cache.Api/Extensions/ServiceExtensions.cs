using Cache.Api.Database.Contexts;
using Cache.Api.Repositories;
using Cache.Api.Services;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Buffers;
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

        services.AddDbContext<AppDbContext>(opt => 
        {
            //opt.EnableSensitiveDataLogging();
            opt.UseNpgsql(connectionString);
            //.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            //.UseSnakeCaseNamingConvention();
            // System.Diagnostics.Debug.WriteLine(comando);
        });
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

        //#pragma warning disable EXTEXP0018
        //services.AddHybridCache(options =>
        //{
        //    options.MaximumPayloadBytes = 1024 * 1024; // 1 MB
        //    options.MaximumKeyLength = 256;
        //    options.DefaultEntryOptions = new HybridCacheEntryOptions
        //    {
        //        Expiration = TimeSpan.FromMinutes(10),
        //        LocalCacheExpiration = TimeSpan.FromMinutes(10)
        //    };
        //});
        //#pragma warning restore EXTEXP0018

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

    public static IServiceCollection AddIdempotency(this IServiceCollection services, IConfiguration configuration)
    {
        //var idempotencyOptions = new IdempotencyOptions();
        //configuration.GetSection("Idempotency").Bind(idempotencyOptions);

        //services.AddRedisIdempotency(configuration, delegate (IdempotencyOptions cfg)
        //{
        //    cfg.HeaderName = "Chave-Idempotencia";
        //    cfg.TTLInHours = idempotencyOptions.TTLInHours;
        //    cfg.Prefix = idempotencyOptions.Prefix;
        //});

        return services;
    }

    public static IServiceCollection AddHealthCheck(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnectionPostgres");
        var connectionStringRedis = configuration.GetValue("ConnectionStrings:Redis", "localhost:6379");

        services.AddHttpClient<HealthCheckService>();

        services.AddHealthChecks()
            .AddNpgSql(connectionString!)
            .AddRedis(connectionStringRedis, name: "Redis", timeout: TimeSpan.FromSeconds(5))
            .AddCheck<HealthCheckService>("Serviço Externo");

        return services;
    }

    public static IApplicationBuilder UseOpenConnection(this IApplicationBuilder app)
    {
        //await using var serviceScope = app.Services.CreateAsyncScope()
        //await using (var dbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>())
        //{
        //    // await dbContext.Database.EnsureCreatedAsync();
        //    await dbContext.OpenConnection();
        //}

        return app;
    }

    public static IApplicationBuilder UseAppCors(this IApplicationBuilder app)
    {
        // return app.UseCors("CorsPolicy");

        app.UseCors(p =>
        {
            p.AllowAnyOrigin();
            p.WithMethods(); // "GET"
            p.AllowAnyHeader();
        });

        return app;
    }

    public static IApplicationBuilder UseIdempotency(this IApplicationBuilder app)
    {
        // app.UseMiddleware<IdempotencyMiddleware>(Array.Empty<object>());

        return app;
    }

    public static IApplicationBuilder UseHealthCheck(this IApplicationBuilder app)
    {
        // app.MapHealthChecks("/health");

        //app.MapHealthChecks("/health", new HealthCheckOptions
        //{
        //    ResponseWriter = async (context, report) =>
        //    {
        //        context.Response.ContentType = "application/json";

        //        var result = new
        //        {
        //            status = report.Status.ToString(),
        //            checks = report.Entries.Select(entry => new
        //            {
        //                name = entry.Key,
        //                status = entry.Value.Status.ToString(),
        //                description = entry.Value.Description
        //            }),
        //            duration = report.TotalDuration
        //        };

        //        await context.Response.WriteAsJsonAsync(result);
        //    }
        //});

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
}
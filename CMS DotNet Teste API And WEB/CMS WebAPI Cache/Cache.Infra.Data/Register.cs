using Cache.Domain;
using Cache.Domain.Models;
using Cache.Domain.Repository;
using Cache.Infra.Data.Context;
using Cache.Infra.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using StackExchange.Redis;
using System.Reflection;

namespace Cache.Infra.Data;

public static class Register
{
    public static IServiceCollection AddDataContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<AppDbContext>(opt =>
        {
            //opt.EnableSensitiveDataLogging();
            opt.UseNpgsql(connectionString);
            //.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            //.UseSnakeCaseNamingConvention();
            // System.Diagnostics.Debug.WriteLine(comando);
            //opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            //opt.UseSqlite(connectionString)
            //opt.UseInMemoryDatabase("TarefasDB")
        });


        //}).AddUnitOfWork<DataContext>().AddScoped<IDataContext>(sp => sp.GetRequiredService<DataContext>());

        //services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        //services.AddTransient<IUserRepository, UserRepository>();

        //services.AddMemoryCache();
        //services.AddSingleton<ICacheService, InMemoryCacheService>();
        var connectionStringRedis = configuration.GetValue("ConnectionStrings:Redis", "localhost:6379");
        //services.AddStackExchangeRedisCache(opt => { opt.Configuration = connectionStringRedis; opt.InstanceName = "RedisDemo_"; });
        //services.AddScoped<IConnectionMultiplexer>(cfg => ConnectionMultiplexer.Connect(connectionStringRedis)});
        services.AddSingleton<IDatabase>(cfg => ConnectionMultiplexer.Connect(connectionStringRedis).GetDatabase());

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

        services.AddScoped<IBaseRepository<PessoaModel>, BaseRepository<PessoaModel>>().AddScoped<IBaseRepositoryTransaction>(sp => sp.GetRequiredService<BaseRepository<PessoaModel>>());
        services.AddScoped<IPessoaQueryRepository, PessoaQueryRepository>();
        services.AddScoped<IPessoaCommandRepository, PessoaCommandRepository>();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        //services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(ICore))!));

        services.AddServiceDomain();

        return services;
    }

    public static IServiceCollection AddDefaultHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnectionPostgres");
        var connectionStringRedis = configuration.GetValue("ConnectionStrings:Redis", "localhost:6379");

        services.AddHttpClient<HealthCheckService>();

        services.AddHealthChecks()
            .AddNpgSql(connectionString!)
            .AddRedis(connectionStringRedis, name: "Redis", timeout: TimeSpan.FromSeconds(5));
            //.AddCheck<HealthCheckService>("Serviço Externo");

        return services;
    }
}
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Rinha.Backend._2024.API.Repositories;
using Rinha.Backend._2024.API.Repositories.Interfaces;
using System.IO.Compression;

namespace Rinha.Backend._2024.API.Extensions;

// AddTransient = VariasVezes           - registra um serviço que é criado cada vez que é solicitado
// AddScoped    = UmVezQdoFazRequisicao - registra um serviço que é criado uma vez por solicitação.
// AddSingleton = UmVezQdoSobeAPI       - registra um serviço que é criado uma única vez durante todo o ciclo de vida do aplicativo

public static class ServiceCollectionsExtensions
{
    public static WebApplicationBuilder AddApiPersistence(this WebApplicationBuilder builder)
    {
        //SQLServer
        //var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionSQLServer");

        ////SQLServer - Configurações do contexto para escrita
        //builder.Services.AddDbContextPool<AppWriteDbContext>(opt =>
        //{
        //    opt.UseSqlServer(connectionString, builder => { builder.CommandTimeout(30); });
        //    //opt.LogTo(Console.WriteLine, LogLevel.Information);
        //    SqlMapper.AddTypeMap(typeof(DateTime), DbType.DateTime2);
        //}).AddScoped<IDataContext>(sp => sp.GetRequiredService<AppWriteDbContext>());

        ////SQLServer - Configurações do contexto para leitura
        //builder.Services.AddDbContextPool<AppReadDbContext>(opt => // AddDbContext // AddDbContextPool // AddPooledDbContextFactory
        //{
        //    opt.UseSqlServer(connectionString, builder => { builder.CommandTimeout(30); });
        //    //opt.LogTo(Console.WriteLine, LogLevel.Information);
        //    opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        //    SqlMapper.AddTypeMap(typeof(DateTime), DbType.DateTime2);
        //}).AddScoped<IDataContext>(sp => sp.GetRequiredService<AppReadDbContext>());

        //builder.Services.AddSqlDataSource(connectionString);
        //builder.Services.AddSqlDataSource(connectionString, setupAction => {  setupAction.EnableVerboseLogging(); });
        //builder.Services.AddTransient<IDbConnection>(db => new SqlConnection(connectionString));
       
        //builder.Services.AddScoped<AppDbContextCon.GetConnection>(sp => async () =>
        //{
        //    var connection = new SqlConnection(connectionString);
        //    await connection.OpenAsync();
        //    return connection;
        //});

        //Postgres
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionPostgres");
        //builder.UseNpgsql(connectionString);
        builder.Services.AddNpgsqlDataSource(connectionString);
        //builder.Services.AddNpgsqlDataSource(connectionString, serviceKey: "write").AddNpgsqlDataSource(connectionString, serviceKey: "read");
        //builder.Services.AddEntityFrameworkNpgsql().AddDbContext<ApiDbContext>(options => options.UseNpgsql(connectionString));

        // builder.Services.AddScoped<IDbConnection>(db => new NpgsqlConnection(connectionString));
        //NpgsqlLogManager.Provider = new ConsoleLoggingProvider(NpgsqlLogLevel.Trace, true, true);

        //builder.Services.AddScoped<IDbConnection>(x => async () =>
        //{
        //    var connection = new NpgsqlConnection(connectionString);
        //    await connection.OpenAsync();
        //    return connection;
        //});

        //SQLite
        //var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionSQLite");
        //builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlite(connectionString));
        //SQLitePCL.raw.SetProvider();
        //SQLitePCL.Batteries.Init();
        //SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());

        //MySQL
        //var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionMySQL");
        //builder.Services.AddDbContext<AppDbContext>(opt => opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        //Memory
        //builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("TarefasDB"));

        builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
        builder.Services.AddScoped<IClienteTransacaoRepository, ClienteTransacaoRepository>();
        builder.Services.AddScoped<IClienteCarteiraRepository, ClienteCarteiraRepository>();

        return builder;
    }

    public static WebApplicationBuilder AddApiCompression(this WebApplicationBuilder builder)
    {
        builder.Services.AddResponseCompression(options =>
        {
            options.EnableForHttps = true;
            options.Providers.Add<BrotliCompressionProvider>();
            options.Providers.Add<GzipCompressionProvider>();
        }).Configure<GzipCompressionProviderOptions>(options => { options.Level = CompressionLevel.Fastest; }) // SmallestSize
          .Configure<BrotliCompressionProviderOptions>(options => { options.Level = CompressionLevel.Fastest; });

        return builder;
    }

    public static WebApplicationBuilder AddApiSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "Catalogo.API", Version = "v1" }); });

        return builder;
    }
}
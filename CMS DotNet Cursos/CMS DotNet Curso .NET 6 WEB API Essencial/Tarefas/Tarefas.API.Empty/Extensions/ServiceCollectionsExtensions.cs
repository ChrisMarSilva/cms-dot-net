using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.IO.Compression;
using Tarefas.Data.Persistence;
using Tarefas.Data.Persistence.Interfaces;
using Tarefas.Data.Repositories;
using Tarefas.Data.Repositories.Interfaces;
using Tarefas.Service;
using Tarefas.Service.Interfaces;
using static Tarefas.Data.Persistence.AppDbContextCon;

namespace Tarefas.API.Empty.Extensions;

public static class ServiceCollectionsExtensions
{
    public static WebApplicationBuilder AddPersistence(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddScoped<GetConnection>(sp => async () =>
        {
            var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();
            return connection;
        });

        // DbContex
        builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("TarefasDB"));
        builder.Services.AddTransient<IUnitofWork, UnitOfWork>();

        // Repository
        builder.Services.AddScoped<ITarefaRepository, TarefaRepository>();

        // Service
        builder.Services.AddScoped<ITarefaService, TarefaService>();

        return builder;
    }

    public static WebApplicationBuilder AddCompression(this WebApplicationBuilder builder)
    {
        builder.Services.AddResponseCompression(options =>
        {
            options.EnableForHttps = true;
            options.Providers.Add<BrotliCompressionProvider>();
            options.Providers.Add<GzipCompressionProvider>();
        });
        builder.Services.Configure<BrotliCompressionProviderOptions>(options => { options.Level = CompressionLevel.Fastest; });
        builder.Services.Configure<GzipCompressionProviderOptions>(options => { options.Level = CompressionLevel.SmallestSize; });

        return builder;
    }
}

using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using Tarefas.Data.Persistence;
using Tarefas.Data.Persistence.Interfaces;
using Tarefas.Data.Repositories.Interfaces;
using Tarefas.Data.Repositories;
using Tarefas.Service.Interfaces;
using Tarefas.Service;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;
using Microsoft.OpenApi.Models;

namespace Tarefas.API.Extensions;

public static class ServiceCollectionsExtensions
{
    public static WebApplicationBuilder AddPersistence(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        //    builder.Services.AddScoped<GetConnection>(sp => async () => {
        //        var connection = new SqlConnection(connectionString);
        //        await connection.OpenAsync();
        //        return connection;
        //    });

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

    public static WebApplicationBuilder AddSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "Tarefas.API", Version = "v1" }); });

        return builder;
    }
}

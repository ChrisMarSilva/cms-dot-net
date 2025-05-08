using Asp.Versioning;
using Cache.Contracts.Response;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Enrichers.Span;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;
using Serilog.Exceptions.EntityFrameworkCore.Destructurers;
using Serilog.Sinks.SystemConsole.Themes;
using System.Diagnostics;
using System.Globalization;
using System.IO.Compression;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.Results;
using System.Buffers;
using System.Reflection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Cache.Shared.Middleware;

public static class Dependencies
{
    public static IServiceCollection AddDefaultResponseCompression(this IServiceCollection services)
    {
        services.AddResponseCompression(options =>
        {
            options.EnableForHttps = true;
            options.Providers.Add<BrotliCompressionProvider>();
            options.Providers.Add<GzipCompressionProvider>();
        }).Configure<GzipCompressionProviderOptions>(options =>
        {
            options.Level = CompressionLevel.Fastest;
        }).Configure<BrotliCompressionProviderOptions>(options =>
        {
            options.Level = CompressionLevel.Fastest;
        });

        return services;
    }

    public static IServiceCollection AddDefaultApiVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
        });

        return services;
    }

    public static IServiceCollection AddDefaultCorsPolicy(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowAnyOrigin()
                    .Build();
            });
        });

        return services;
    }

    public static IServiceCollection AddDefaultIdempotency(this IServiceCollection services, IConfiguration configuration)
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

    public static IServiceCollection AddDefaultMetricsPrometheus(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.GetValue("Metrics:Enabled", false))
        {
            if (configuration.GetValue("Metrics:ActivateSystemMetrics", false))
            {
                // services.AddSystemMetrics(configuration.GetValue("Metrics:RegisterDefaultCollectorsForSystemMetrics", false));
            }
        }

        return services;
    }

    private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    public static IApplicationBuilder UseDefaultExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                var erro = ErrorResponseDto.Iniciar(HttpStatusCode.InternalServerError, mensagem: "Falha interna durante o processamento. Tente novamente.");

                await context.Response.WriteAsync(JsonSerializer.Serialize(erro, _jsonSerializerOptions));
            });
        });

        return app;
    }

    public static IApplicationBuilder UseDefaultStatusCodePages(this IApplicationBuilder app)
    {
        app.UseStatusCodePages(async context =>
        {
            var erro = ErrorResponseDto.Iniciar((HttpStatusCode)context.HttpContext.Response.StatusCode, ReasonPhrases.GetReasonPhrase(context.HttpContext.Response.StatusCode));

            context.HttpContext.Response.ContentType = "application/json";
            await context.HttpContext.Response.WriteAsync(JsonSerializer.Serialize(erro, _jsonSerializerOptions));
        });

        return app;
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

    public static IApplicationBuilder UseDefaultCors(this IApplicationBuilder app)
    {
        return app.UseCors("CorsPolicy");
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

    public static IApplicationBuilder UseMetricsPrometheus(this IApplicationBuilder app, IConfiguration configuration)
    {
        if (configuration.GetValue("Metrics:Enabled", false))
        {
            //app.UseMetricServer();
            //app.UseHttpMetrics();
        }

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

public static class ConfigureSerilog
{
    public static void CreateLogger<T>() where T : class
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
                          ?? Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")
                          ?? "Development";

        var configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, false)
            .AddJsonFile($"appsettings.{environment}.json", true, false)
            .AddEnvironmentVariables("JDPI_");

        if (environment.Equals("Development", StringComparison.OrdinalIgnoreCase))
        {
            configurationBuilder.AddUserSecrets<T>(true);
        }

        var configuration = configurationBuilder.Build();

        // Create the logger
        var loggerConfiguration = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .Enrich.WithAssemblyName<T>()
            .Enrich.WithProperty("Environment", environment)
            .Enrich.WithProperty("ProcessorCount", Environment.ProcessorCount)
            .Enrich.WithEnvironmentUserName()
            .Enrich.WithProcessId()
            .Enrich.WithProcessName()
            .Enrich.WithSpan(new SpanOptions
            {
                IncludeBaggage = true,
                IncludeOperationName = true,
                IncludeTags = true,
                IncludeTraceFlags = true
            });

        loggerConfiguration.Enrich.WithAssemblyVersion<T>(useSemVer: true);
        try
        {
            if (FileVersionInfo.GetVersionInfo(typeof(T).Assembly.Location).ProductVersion is { } productVersion)
                loggerConfiguration.Enrich.WithProperty("AssemblyProductVersion", productVersion);
        }
        catch
        {
            /* ignored */
        }

        if (configuration.GetValue("Serilog:ExceptionDetailsEnabled", false))
        {
            loggerConfiguration = loggerConfiguration
                .Enrich.WithExceptionDetails(new DestructuringOptionsBuilder()
                    .WithDefaultDestructurers()
                    .WithIgnoreStackTraceAndTargetSiteExceptionFilter()
                    .WithDestructurers([new DbUpdateExceptionDestructurer()]));
        }

        loggerConfiguration
            .ReadFrom.Configuration(configuration)
            //.WriteTo.Tracing()
            //.WriteTo.File("logs/log.txt", LogEventLevel.Warning, rollingInterval: RollingInterval.Day, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}"));
            .WriteTo.Async(a =>
            {
                //a.Console(LogEventLevel.Verbose, "{NewLine}{Timestamp:HH:mm:ss.fff} [{Level}] ({CorrelationToken}) {Message:lj}{NewLine}{Exception} {Properties:j}", theme: AnsiConsoleTheme.Literate, formatProvider: new CultureInfo("pt-BR"));
                a.Console(LogEventLevel.Verbose, "{NewLine}{Timestamp:HH:mm:ss.fff} [{Level}] {Message:lj}", theme: AnsiConsoleTheme.Literate, formatProvider: new CultureInfo("pt-BR"));
            }, bufferSize: 500);

        Log.Logger = loggerConfiguration.CreateLogger();
    }

    public static void CloseAndFlush()
    {
        Log.CloseAndFlush();
    }
}
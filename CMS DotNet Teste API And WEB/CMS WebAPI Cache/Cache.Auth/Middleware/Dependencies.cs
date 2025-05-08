using Asp.Versioning;
using Microsoft.AspNetCore.ResponseCompression;
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

namespace Cache.Auth.Middleware;

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

    public static IApplicationBuilder UseDefaultCors(this IApplicationBuilder app)
    {
        return app.UseCors("CorsPolicy");
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

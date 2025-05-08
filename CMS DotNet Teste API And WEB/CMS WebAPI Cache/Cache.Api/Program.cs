using Cache.Infra.Bootstrap;
using Cache.Shared.Filters;
using Cache.Shared.Middleware;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;
using System.Text.Json.Serialization;

ConfigureSerilog.CreateLogger<Program>();
try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog();

    builder.Configuration
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", false, false)
        .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, false)
        .AddEnvironmentVariables();

    builder.Services.AddControllers(configure =>
    {
        configure.Filters.Add<ValidateModelFilterAttribute>(-9999);
    }).AddJsonOptions(opt =>
    {
        //opt.JsonSerializerOptions.PropertyNamingPolicy = null;
        //opt.JsonSerializerOptions.WriteIndented = true;
        //opt.JsonSerializerOptions.AllowTrailingCommas = true;
        opt.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });
    builder.Services.AddOpenApi();
    builder.Services.AddDefaultResponseCompression();
    builder.Services.AddDefaultApiVersioning();
    builder.Services.AddDefaultCorsPolicy();
    builder.Services.AddDefaultIdempotency(builder.Configuration);
    // builder.Services.AddDefaultHealthChecks(builder.Configuration);
    builder.Services.AddDefaultMetricsPrometheus(builder.Configuration);
    builder.Services.AddServicesForApi(builder.Configuration);

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
        //app.UseOpenConnection();

        //await using (var serviceScope = app.Services.CreateAsyncScope())
        //await using (var dbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>())
        //{
        //    // await dbContext.Database.EnsureCreatedAsync();
        //    await dbContext.OpenConnection();
        //}
    }
    else
    {
        app.UseDefaultExceptionHandler()
           .UseDefaultStatusCodePages();
    }
    app.UseResponseCompression();
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.UseDefaultCors();
    app.UseIdempotency();
    app.UseHealthCheck();
    //app.UseMetricsPrometheus(builder.Configuration);
    app.MapControllers();
    //if (builder.Configuration.GetValue("Metrics:Enabled", false))
    //    app.MapMetrics();

    //await using (var localScope = app.Services.CreateAsyncScope())
    //{
    //    await localScope.ServiceProvider.GetRequiredService<IDataContext>().OpenConnection();
    //}

    app.MapHealthChecks("/health", new HealthCheckOptions
    {
        ResponseWriter = async (context, report) =>
        {
            context.Response.ContentType = "application/json";

            var result = new
            {
                status = report.Status.ToString(),
                checks = report.Entries.Select(entry => new
                {
                    name = entry.Key,
                    status = entry.Value.Status.ToString(),
                    description = entry.Value.Description
                }),
                duration = report.TotalDuration
            };

            await context.Response.WriteAsJsonAsync(result);
        }
    });

    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host encerrado inesperadamente");
}
finally
{
    ConfigureSerilog.CloseAndFlush();
}
using Cache.Api.Database.Contexts;
using Cache.Api.Extensions;
using Cache.Api.Filters;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Prometheus;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add<ValidateModelFilterAttribute>(-9999);
    //opt.Filters.Add<ValidateIdempotencyKeyFilterAttribute>();
}).AddJsonOptions(opt => 
{ 
    opt.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull; 
});

builder.Services.AddOpenApi();
builder.Services.AddCompression();
builder.Services.AddInfra(builder.Configuration);
builder.Services.AddServices();
builder.Services.AddCors();
builder.Services.AddIdempotency(builder.Configuration);
builder.Services.AddHealthCheck(builder.Configuration);
builder.Services.AddMetricsPrometheus(builder.Configuration);

//builder.WebHost.UseUrls("http://0.0.0.0:5042"); // Configure o Kestrel para aceitar conexões de qualquer IP

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseOpenConnection();

    await using (var serviceScope = app.Services.CreateAsyncScope())
    await using (var dbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>())
    {
        // await dbContext.Database.EnsureCreatedAsync();
        await dbContext.OpenConnection();
    }
}

app.UseResponseCompression();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseAppCors();
app.UseIdempotency();
app.UseHealthCheck();
app.UseMetricsPrometheus(builder.Configuration);

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

if (builder.Configuration.GetValue("Metrics:Enabled", false))
    app.MapMetrics();

app.Run();
using Rinha.Backend._2024.API.Endpoints;
using Rinha.Backend._2024.API.Extensions;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

builder.Services.AddControllers().AddJsonOptions(options => { options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull; });
builder.AddApiSwagger();
builder.AddApiCompression();
builder.AddApiPersistence();
builder.Services.AddCors();

builder.Services.Configure<HostOptions>(cfg =>
{
    cfg.ShutdownTimeout = TimeSpan.FromSeconds(35);
    cfg.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.Ignore;
});

var app = builder.Build();

app.UseResponseCompression();
app.UseHttpsRedirection();
app.UseMapClientesEndpoints();
app.UseExceptionHandling(app.Environment);
app.UseSwaggerMiddleware(app.Environment);
app.UseAppCors(); //app.UseDefaultCors();

app.Run();


using Cache.Auth.Middleware;
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

    builder.Services.AddControllers().AddJsonOptions(opt => { opt.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull; });
    builder.Services.AddOpenApi();
    builder.Services.AddDefaultResponseCompression();
    builder.Services.AddDefaultApiVersioning();
    builder.Services.AddDefaultCorsPolicy();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }

    app.UseResponseCompression();
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.UseDefaultCors();
    app.MapControllers();

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
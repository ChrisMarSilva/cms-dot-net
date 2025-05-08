using Cache.Infra.Bootstrap;
using Cache.Infra.Bootstrap.Filters;
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
        opt.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });
    builder.Services.AddOpenApi();
    builder.Services.AddServicesForApi(builder.Configuration);

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }
    else
    {
        app.UseDefaultExceptionHandler()
           .UseDefaultStatusCodePages();
    }
    app.UseResponseCompression();
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.UseServicesForApi();
    app.MapControllers();

    //await using (var serviceScope = app.Services.CreateAsyncScope())
    //{
    //    await serviceScope.ServiceProvider.GetRequiredService<IDataContext>().OpenConnection();
    //}

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
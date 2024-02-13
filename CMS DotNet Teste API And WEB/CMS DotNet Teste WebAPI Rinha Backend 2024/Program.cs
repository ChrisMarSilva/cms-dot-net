using Rinha.Backend._2024.API.Context.Interfaces;
using Rinha.Backend._2024.API.Endpoints;
using Rinha.Backend._2024.API.Extensions;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull; });

builder.AddApiSwagger();
builder.AddApiCompression();
builder.AddApiPersistence();

builder.Services.AddCors();
//builder.Services.AddCors(options => { options.AddPolicy("CorsPolicy", b => { b.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin().Build(); }); });

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

app.UseAppCors();
//app.UseCors("CorsPolicy");

await using (var localScope = app.Services.CreateAsyncScope())
{
    await localScope.ServiceProvider.GetRequiredService<IDataContext>().OpenConnection();
}

app.Run();

// dotnet tool install --global dotnet-ef
// dotnet tool update --global dotnet-ef
// dotnet build 

// dotnet ef migrations add AddTablesInitOnDataTablesDb
// dotnet ef database update
// dotnet ef migrations remove

using Catalogo.Data.Persistence;
using Catalogo.Data.Persistence.Interfaces;
using Catalogo.Data.Repositories;
using Catalogo.Data.Repositories.Interfaces;
using Catalogo.Service;
using Catalogo.Service.Interfaces;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.IO.Compression;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// ConnectionString
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];
// var connectionString = builder.Configuration.GetSection("ConnectionStrings").GetValue<string>("DefaultConnection");

// DbContex
// builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(connectionStr, new MySqlServerVersion(new Version(5, 6, 0))));
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddTransient<IUnitofWork, UnitOfWork>();

// Repository
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();

// Service
builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();

builder.Services.AddControllers()
    .AddJsonOptions(options => {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        //options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); // serialize enums as strings in api responses (e.g. Role)
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, false));
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault; // ignore omitted parameters on models to enable optional params (e.g. User update)
        // options.JsonSerializerOptions.IgnoreNullValues = true;
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "Catalogo.API", Version = "v1" }); });

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
});
builder.Services.Configure<BrotliCompressionProviderOptions>(options => { options.Level = CompressionLevel.Fastest; });
builder.Services.Configure<GzipCompressionProviderOptions>(options => { options.Level = CompressionLevel.SmallestSize; });


var app = builder.Build();

// using var scope = builder.Services.BuildServiceProvider().CreateScope();
//using var scope = app.Services.CreateScope();
//using var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
//// await context.Init();
//// context.Database.Migrate();
//context.Database.EnsureCreated();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseResponseCompression();
app.UseAuthorization();
app.MapControllers();

app.Run();

// dotnet tool install --global dotnet-ef
// dotnet tool update --global dotnet-ef
// dotnet build 

// dotnet ef 
// dotnet ef migrations add AddTablesInitOnDataTablesDb
// dotnet ef migrations add AddDadosTables01
// dotnet ef migrations add PopulaCategorias
// dotnet ef migrations add PopulaCategorias02
// dotnet ef migrations add PopulaProdutos
// dotnet ef database update
// dotnet ef migrations remove

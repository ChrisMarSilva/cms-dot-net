using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.OpenApi.Models;
using PokeAcademy.API.Controllers;
using PokeAcademy.API.Services.Interfaces;
using Refit;
using System.IO.Compression;

var builder = WebApplication.CreateBuilder(args);

const string BASE_URL = "https://pokeapi.co/api/v2"; // builder.Configuration["ServiceUrls:ProductAPI"]

//builder.Services.AddHttpClient();
builder.Services.AddHttpClient<PokemonsController>(c => c.BaseAddress = new Uri(BASE_URL));

builder.Services.AddRefitClient<IPokeService>().ConfigureHttpClient(c => c.BaseAddress = new Uri(BASE_URL));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>{c.SwaggerDoc("v1", new OpenApiInfo { Title = "PokeAcademy.API", Version = "v1" });});

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
});
builder.Services.Configure<BrotliCompressionProviderOptions>(options => { options.Level = CompressionLevel.Fastest; });
builder.Services.Configure<GzipCompressionProviderOptions>(options => { options.Level = CompressionLevel.SmallestSize; });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseResponseCompression();
app.MapControllers();

app.Run();

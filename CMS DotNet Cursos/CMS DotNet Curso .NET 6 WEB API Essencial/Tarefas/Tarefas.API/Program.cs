using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.IO.Compression;
using Tarefas.Data.Persistence;
using Tarefas.Data.Persistence.Interfaces;
using Tarefas.Data.Repositories;
using Tarefas.Data.Repositories.Interfaces;
using Tarefas.Service;
using Tarefas.Service.Interfaces;
using System.Data.SqlClient;
using Tarefas.Domain.Models;

var builder = WebApplication.CreateBuilder(args);

// builder.AddPersistence();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "Tarefas.API", Version = "v1" }); });

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
});
builder.Services.Configure<BrotliCompressionProviderOptions>(options => { options.Level = CompressionLevel.Fastest; });
builder.Services.Configure<GzipCompressionProviderOptions>(options => { options.Level = CompressionLevel.SmallestSize; });

// DbContex
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddScoped<GetConnection>(sp => async () =>
//{
//    var connection = new SqlConnection(connectionString);
//    await connection.OpenAsync();
//    return connection;
//});
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("TarefasDB"));
builder.Services.AddTransient<IUnitofWork, UnitOfWork>();

// Repository
builder.Services.AddScoped<ITarefaRepository, TarefaRepository>();

// Service
builder.Services.AddScoped<ITarefaService, TarefaService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// app.MapTarefasEndpoints();

app.MapGet("/", () => "Olá Mundo");
app.MapGet("frases", async () => await new HttpClient().GetStringAsync("https://ron-swanson-quotes.herokuapp.com/v2/quotes") );

app.MapGet("/tarefas", async (AppDbContext ctx) => {
    var tarefas = await ctx.Tarefas.ToListAsync(); //  ?? new Tarefa()
    if (tarefas is null || !tarefas.Any())
        return Results.NotFound();
    return Results.Ok(tarefas);
});
app.MapGet("/tarefas/{id:Guid}", async (AppDbContext ctx, Guid id) => {
    var tarefa = await ctx.Tarefas.FirstOrDefaultAsync(d => d.Id == id); //  ?? new Tarefa()
    if (tarefa is null)
        return Results.NotFound();
    return Results.Ok(tarefa);
});
app.MapPost("/tarefas", async (AppDbContext ctx, Tarefa input) => {
    await ctx.Tarefas.AddAsync(input);
    await ctx.SaveChangesAsync();
    return Results.Created($"/tarefas/{input.Id}", input);
});
app.MapPut("/tarefas/{id:Guid}", async (AppDbContext ctx, Guid id, Tarefa input) => {
    var tarefa = await ctx.Tarefas.FirstOrDefaultAsync(d => d.Id == id);
    if (tarefa is null)
        return Results.NotFound();
    tarefa.Update(atividade: input.Atividade, status: input.Status);
    ctx.Tarefas.Update(tarefa);
    await ctx.SaveChangesAsync();
    return Results.NoContent();
});
app.MapDelete("/tarefas/{id:Guid}", async (AppDbContext ctx, Guid id) => {

    var tarefa = await ctx.Tarefas.FirstOrDefaultAsync(d => d.Id == id);
    if (tarefa is null)
        return Results.NotFound();
    ctx.Tarefas.Remove(tarefa);
    await ctx.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();

// dotnet tool install --global dotnet-ef
// dotnet tool update --global dotnet-ef
// dotnet build 

// dotnet ef 
// dotnet ef migrations add AddTablesInitOnDataTablesDb
// dotnet ef database update
// dotnet ef migrations remove

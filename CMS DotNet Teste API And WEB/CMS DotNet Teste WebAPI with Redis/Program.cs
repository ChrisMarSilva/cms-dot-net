using CMS_DotNet_Teste_WebAPI_with_Redis.Repositories;
using CMS_DotNet_Teste_WebAPI_with_Redis.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMemoryCache();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "RedisDemo_";
});

builder.Services.AddSingleton<IProdutoRepository, ProdutoRepository>(); // AddSingleton = UmVezQdoSobeAPI - registra um servi�o que � criado uma �nica vez durante todo o ciclo de vida do aplicativo
//builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>(); // UmVezQdoFazRequisicao - registra um servi�o que � criado uma vez por solicita��o.

builder.Services.AddScoped<IProdutoService, ProdutoService>(); // UmVezQdoFazRequisicao - registra um servi�o que � criado uma vez por solicita��o.
//builder.Services.Decorate<ICarStore, CarCachingStore>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

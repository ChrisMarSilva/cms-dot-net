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

builder.Services.AddSingleton<IProdutoRepository, ProdutoRepository>(); // AddSingleton = UmVezQdoSobeAPI - registra um serviço que é criado uma única vez durante todo o ciclo de vida do aplicativo
//builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>(); // UmVezQdoFazRequisicao - registra um serviço que é criado uma vez por solicitação.

builder.Services.AddScoped<IProdutoService, ProdutoService>(); // UmVezQdoFazRequisicao - registra um serviço que é criado uma vez por solicitação.
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

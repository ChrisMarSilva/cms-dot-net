using Catalogo.API.ApiEndpoints;
using Catalogo.API.AppServicesExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddApiSwagger();
builder.AddPersistence();
builder.Services.AddCors();
builder.AddAutenticationJwt();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapAutenticacaoEndpoints();
app.MapCategoriasEndpoints();
app.MapProdutosEndpoints();

var environment = app.Environment;
app.UseExceptionHandling(environment);
app.UseSwaggerMiddleware(environment);

app.UseAppCors();
app.UseAuthentication();
app.UseAuthorization();

app.Run();

// dotnet tool install --global dotnet-ef
// dotnet tool update --global dotnet-ef

// dotnet build 
// dotnet ef 

// dotnet ef migrations add AddTablesInitOnDataTablesDb

// dotnet ef database update 
// dotnet ef database update AddTablesInitOnDataTablesDb

// dotnet ef migrations remove 
// dotnet ef migrations remove AddTablesInitOnDataTablesDb

using Tarefas.API.Endpoints;
using Tarefas.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.AddSwagger();
builder.AddCompression();
builder.AddPersistence();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMapTarefasEndpoints();
app.UseMapOutrosEndpoints();

app.Run();

// dotnet tool install --global dotnet-ef
// dotnet tool update --global dotnet-ef
// dotnet build 

// dotnet ef 
// dotnet ef migrations add AddTablesInitOnDataTablesDb
// dotnet ef database update
// dotnet ef migrations remove

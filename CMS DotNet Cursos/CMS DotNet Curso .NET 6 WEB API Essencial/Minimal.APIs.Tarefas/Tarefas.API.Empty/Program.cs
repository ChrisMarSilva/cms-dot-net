using Tarefas.API.Empty.Endpoints;
using Tarefas.API.Empty.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddCompression();
builder.AddPersistence();

var app = builder.Build();

app.UseMapOutrosEndpoints();
app.UseMapTarefasV1Endpoints();
app.UseMapTarefasV2Endpoints();
app.UseMapTarefasV3Endpoints();

app.Run();

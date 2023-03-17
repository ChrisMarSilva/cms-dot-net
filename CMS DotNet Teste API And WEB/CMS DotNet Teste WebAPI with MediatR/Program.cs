using CMS_DotNet_Teste_WebAPI_with_MediatR.Extensions;
using CMS_DotNet_Teste_WebAPI_with_MediatR.Requests;
using CMS_DotNet_Teste_WebAPI_with_MediatR.Services;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddMediatR(x => x.AsScoped(), typeof(Program));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
// builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddScoped<GuidService>();
builder.Services.AddSingleton<WeatherService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MediateGet<ExempleRequest>("/exemple/{name}");
app.MediateGet<WeatherRequest>("/weather/{city}");

app.Run();

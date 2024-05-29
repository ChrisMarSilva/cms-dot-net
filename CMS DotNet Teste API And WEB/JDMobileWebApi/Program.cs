using JDMobileWebApi;
using JDMobileWebApi.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyAllowedOrigins",
        policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});
builder.Services.AddSignalR();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

var chaves = new List<ChaveModel>
{
    new ChaveModel { Chave = "12345678", Ispb = "12345678", NomeBanco = "Banco A", TipoPessoa = "Física", Documento = "11111111111", Agencia = "0001", Conta = "12345-6", TipoConta = "Corrente", Nome = "João Silva" },
    new ChaveModel { Chave = "87654321", Ispb = "87654321", NomeBanco = "Banco B", TipoPessoa = "Jurídica", Documento = "22222222000199", Agencia = "0002", Conta = "65432-1", TipoConta = "Poupança", Nome = "Empresa X" },
    new ChaveModel { Chave = "11223344",  Ispb = "11223344", NomeBanco = "Banco C", TipoPessoa = "Física", Documento = "33333333333", Agencia = "0003", Conta = "98765-4", TipoConta = "Corrente", Nome = "Maria Souza" },
    new ChaveModel { Chave = "44332211",  Ispb = "44332211", NomeBanco = "Banco D", TipoPessoa = "Jurídica", Documento = "44444444000188", Agencia = "0004", Conta = "12321-0", TipoConta = "Poupança", Nome = "Empresa Y" },
    new ChaveModel { Chave = "55667788", Ispb = "55667788", NomeBanco = "Banco E", TipoPessoa = "Física", Documento = "55555555555", Agencia = "0005", Conta = "56789-3", TipoConta = "Corrente", Nome = "Carlos Pereira" },
    new ChaveModel { Chave = "88776655", Ispb = "88776655", NomeBanco = "Banco F", TipoPessoa = "Jurídica", Documento = "66666666000177", Agencia = "0006", Conta = "43210-9", TipoConta = "Poupança", Nome = "Empresa Z" },
    new ChaveModel { Chave = "99887766", Ispb = "99887766", NomeBanco = "Banco G", TipoPessoa = "Física", Documento = "77777777777", Agencia = "0007", Conta = "67890-2", TipoConta = "Corrente", Nome = "Ana Costa" },
    new ChaveModel { Chave = "66778899",Ispb = "66778899", NomeBanco = "Banco H", TipoPessoa = "Jurídica", Documento = "88888888000166", Agencia = "0008", Conta = "32109-8", TipoConta = "Poupança", Nome = "Empresa W" },
    new ChaveModel { Chave = "77665544",  Ispb = "77665544", NomeBanco = "Banco I", TipoPessoa = "Física", Documento = "99999999999", Agencia = "0009", Conta = "89012-7", TipoConta = "Corrente", Nome = "Paulo Lima" },
    new ChaveModel { Chave = "55443322", Ispb = "55443322", NomeBanco = "Banco J", TipoPessoa = "Jurídica", Documento = "00000000000155", Agencia = "0010", Conta = "10987-6", TipoConta = "Poupança", Nome = "Empresa V" }
};

app.MapGet("/api/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index => new WeatherForecast  ( DateOnly.FromDateTime(DateTime.Now.AddDays(index)),  Random.Shared.Next(-20, 55), summaries[Random.Shared.Next(summaries.Length)] )).ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapGet("/api/chave", ([FromQuery] string chave) =>
{
    var item = chaves.FirstOrDefault(i => i.Chave == chave);
    return item;
})
.WithName("Chave")
.WithOpenApi();

app.UseCors("MyAllowedOrigins");
app.MapHub<ChatHub>("/Chat");
app.Run();


using CMS_DotNet_Teste_Resquest_Http_FoxBit_Api.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = "Server=localhost;Port=3306;Database=tamo_na_bolsa_foxbit;Uid=root;Pwd=Chrs8723;Persist Security Info=False;Connect Timeout=300;Connection Reset=False;Max Pool Size=300;";
builder.Services.AddDbContextPool<AppDbContext>(opt => opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

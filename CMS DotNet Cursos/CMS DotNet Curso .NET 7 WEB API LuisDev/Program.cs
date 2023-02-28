using AwesomeDevEvents.API.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// var connectionString = builder.Configuration.GetConnectionString("DevEventsCs");
// builder.Services.AddDbContext<DevEventsDbContext>(o => o.UseSqlServer(connectionString));
builder.Services.AddSingleton<DevEventsDbContext>();

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

// dotnet tool update --global dotnet-ef
// dotnet build 

// dotnet ef migrations add xxxxx
// dotnet ef database update
// dotnet ef migrations remove

using AutoMapper;
using AwesomeDevEvents.API.Config;
using AwesomeDevEvents.API.Persistence;
using AwesomeDevEvents.API.Persistence.Interfaces;
using AwesomeDevEvents.API.Repositories;
using AwesomeDevEvents.API.Repositories.Interfaces;
using AwesomeDevEvents.API.Services;
using AwesomeDevEvents.API.Services.Interfaces;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Sinks.MSSqlServer;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<ApplicationDbContext>(o => o.UseSqlServer(connectionString));
builder.Services.AddSqlServer<ApplicationDbContext>(connectionString);

builder.Services.AddTransient<IUnitofWork, UnitOfWork>();

builder.Services.AddScoped<IDevEventRepository, DevEventRepository>();
builder.Services.AddScoped<IDevEventSpeakerRepository, DevEventSpeakerRepository>();

builder.Services.AddScoped<IDevEventService, DevEventService>();

IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//builder.Services.AddAutoMapper(typeof(DevEventProfile));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.WebHost.UseSerilog((context, configuration) =>
{
    configuration
        .WriteTo.Console()
        .WriteTo.MSSqlServer(
            // context.Configuration["ConnectionStrings:DefaultConnection"],
            //context.Configuration.GetSection("ConnectionStrings").GetValue<string>("DefaultConnection"),
            connectionString,
            sinkOptions: new MSSqlServerSinkOptions()
            {
                AutoCreateSqlTable = true,
                TableName = "LogAPI"
            });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.UseExceptionHandler("/error");

app.Map("/error", (HttpContext http) =>
{
    var error = http.Features?.Get<IExceptionHandlerFeature>()?.Error;
    if (error != null)
    {
        if (error is SqlException)
            return Results.Problem(title: "Database out", statusCode: 500);
        if (error is BadHttpRequestException)
            return Results.Problem(title: "Error to convert data to other type. See all the information sent", statusCode: 500);
    }
    return Results.Problem(title: "An error ocurred", statusCode: 500);
});

app.Run();

// dotnet tool install --global dotnet-ef
// dotnet tool update --global dotnet-ef
// dotnet build 

// dotnet ef migrations add AddTablesInitOnDataTablesDb
// dotnet ef database update
// dotnet ef migrations remove

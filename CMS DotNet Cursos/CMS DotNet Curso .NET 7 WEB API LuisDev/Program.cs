using AutoMapper;
using AwesomeDevEvents.API.Config;
using AwesomeDevEvents.API.Persistence;
using AwesomeDevEvents.API.Persistence.Interfaces;
using AwesomeDevEvents.API.Repositories;
using AwesomeDevEvents.API.Repositories.Interfaces;
using AwesomeDevEvents.API.Services;
using AwesomeDevEvents.API.Services.Interfaces;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.IO.Compression;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<ApplicationDbContext>(o => o.UseSqlServer(connectionString));
builder.Services.AddSqlServer<ApplicationDbContext>(connectionString);

builder.Services.AddTransient<IUnitofWork, UnitOfWork>();

builder.Services.AddScoped<IDevEventRepository, DevEventRepository>();
builder.Services.AddScoped<IDevEventSpeakerRepository, DevEventSpeakerRepository>();

builder.Services.AddScoped<IDevEventService, DevEventService>();
// builder.Services.AddTransient<IDevEventService, DevEventService>();

IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//builder.Services.AddAutoMapper(typeof(DevEventProfile));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
});
builder.Services.Configure<BrotliCompressionProviderOptions>(options => { 
    options.Level = CompressionLevel.Fastest; 
});
builder.Services.Configure<GzipCompressionProviderOptions>(options => { 
    options.Level = CompressionLevel.SmallestSize; 
});

builder.Services.AddRateLimiter(_ => _
    .AddFixedWindowLimiter("fixed", options =>
    {
        options.PermitLimit = 5;
        options.Window = TimeSpan.FromSeconds(10);
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = 2;
    })
    .AddSlidingWindowLimiter("sliding", options =>
    {
        options.PermitLimit = 5;
        options.Window = TimeSpan.FromSeconds(10);
        options.SegmentsPerWindow = 5;
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = 2;
    }));

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
app.UseResponseCompression();
app.UseAuthorization();
app.UseRateLimiter();
app.UseExceptionHandler("/error");
app.MapControllers();

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

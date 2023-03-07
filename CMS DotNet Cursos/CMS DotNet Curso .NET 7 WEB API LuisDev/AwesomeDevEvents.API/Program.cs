using AutoMapper;
using AwesomeDevEvents.API.Config;
using AwesomeDevEvents.Infrastructure.Persistence;
using AwesomeDevEvents.Infrastructure.Persistence.Interfaces;
using AwesomeDevEvents.Infrastructure.Repositories;
using AwesomeDevEvents.Infrastructure.Repositories.Interfaces;
using AwesomeDevEvents.Service;
using AwesomeDevEvents.Service.Interfaces;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.IO.Compression;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];
// var connectionString = builder.Configuration.GetSection("ConnectionStrings").GetValue<string>("DefaultConnection");

// builder.Services.AddDbContext<ApplicationDbContext>(o => o.UseSqlServer(connectionString));
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

builder.Services.AddMemoryCache();
// builder.Services.AddSingleton<MyMemoryCache>();

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

// WebHost

//builder.Host.UseSerilog((ctx, config) => {
//    config
//        .WriteTo.Console()
//        .MinimumLevel.Information();
//});

//var appSettings = new ConfigurationBuilder()
//    .SetBasePath(Directory.GetCurrentDirectory())
//    .AddJsonFile("appsettings.json")
//    .Build();

//var sinkOpts = new MSSqlServerSinkOptions();
//sinkOpts.AutoCreateSqlTable = true;
//sinkOpts.SchemaName = "dbo";
//sinkOpts.TableName = "LogsAPI";
//sinkOpts.batchPostingLimit = 1000;
//sinkOpts.period = "0.00:00:30";

//var columnOpts = new ColumnOptions();
//columnOpts.Store.Remove(StandardColumn.Properties);
//columnOpts.Store.Add(StandardColumn.LogEvent);
//columnOpts.LogEvent.DataLength = 2048;
//columnOpts.PrimaryKey = columnOpts.TimeStamp;
//columnOpts.TimeStamp.NonClusteredIndex = true;

//CREATE TABLE[Logs] (
//   [Id] int IDENTITY(1,1) NOT NULL,
//   [Message] nvarchar(max) NULL,
//   [MessageTemplate] nvarchar(max) NULL,
//   [Level] nvarchar(128) NULL,
//   [TimeStamp] datetime NOT NULL,
//   [Exception] nvarchar(max) NULL,
//   [Properties] nvarchar(max) NULL
//   CONSTRAINT[PK_Logs] PRIMARY KEY CLUSTERED ([Id] ASC)
//);

//builder.Host.UseSerilog((ctx, config) => {
//    config
//        .WriteTo.Console()
//        //.WriteTo.Console(new JsonFormatter())
//        // .WriteTo.Console(restrictedToMinimumLevel:Serilog.Events.LogEventLevel.Information)
//        //.WriteTo.Seq("http://localhost:5341")
//        //.WriteTo.File("log.txt")
//        //.WriteTo.File(new JsonFormatter(), "log.txt")
//        //.WriteTo.File(new CompactJsonFormatter(), "Log.json", rollingInterval: RollingInterval.Day)
//        .WriteTo.MSSqlServer(connectionString: connectionString, sinkOptions: sinkOpts, columnOptions: columnOpts, appConfiguration: appSettings)
//        .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning);
//});

builder.Host.UseSerilog((ctx, config) => config.ReadFrom.Configuration(ctx.Configuration));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();
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

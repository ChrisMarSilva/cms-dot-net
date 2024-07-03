using MassTransit;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Project.Database.Context;
using Project.Database.Repositories;
using Project.Database.Repositories.Interfaces;
using Project.Domain.Dtos.Response;
using Project.Filters;
using Project.Filters.Idempotency;
using Project.Service;
using Project.Service.Interfaces;
using Project.ServiceBus.Commands;
using Project.ServiceBus.Consumers;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core.Abstractions;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Core.Implementations;
using System.IO.Compression;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers(configure => { configure.Filters.Add<ValidateModelFilterAttribute>(-9999); })
    .AddJsonOptions(options => { options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;});

builder.Services.AddEndpointsApiExplorer();

var configuration = builder.Configuration;

builder.Services
    .AddDbContext<ApplicationDbContext>(opt =>
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        opt.UseSqlServer(connectionString, builder => { builder.CommandTimeout(30); });
    })
    .AddScoped<IDataContext>(sp => sp.GetRequiredService<ApplicationDbContext>());

builder.Services.AddScoped<IRepository, BaseRepository>();// .AddScoped<IRepositoryTransaction>(sp => sp.GetRequiredService<BaseRepository>());
builder.Services.AddScoped<IQueryRepository, QueryRepository>();
builder.Services.AddScoped<ICommandRepository, CommandRepository>();
builder.Services.AddScoped<IService, Service>();
//builder.Services.AddHostedService<IWorkerService, WorkerService>();

builder.Services.AddResponseCompression(opt =>
{
    opt.EnableForHttps = true;
    opt.Providers.Add<GzipCompressionProvider>();
    opt.Providers.Add<BrotliCompressionProvider>();
});

builder.Services.Configure<BrotliCompressionProviderOptions>(opt => {
    opt.Level = CompressionLevel.Fastest;
});

builder.Services.Configure<GzipCompressionProviderOptions>(opt => {
    opt.Level = CompressionLevel.Fastest; 
});

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin().Build();
    });
});

builder.Services.AddMassTransit(configure =>
{
    configure.SetKebabCaseEndpointNameFormatter();
    //configure.AddConsumers(typeof(Program).Assembly);
    configure.AddConsumer<ClientConsumer>();

    configure.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(configuration.GetValue("RabbitMQ:Host", "localhost"), configuration.GetValue<ushort>("RabbitMQ:Port", 5672), configuration.GetValue("RabbitMQ:VirtualHost", "/"), c =>
        {
            c.Username(configuration.GetValue("RabbitMQ:Username", "guest")!);
            c.Password(configuration.GetValue("RabbitMQ:Password", "guest")!);

            c.ConfigureBatchPublish(b =>
            {
                b.Enabled = true;
                b.Timeout = TimeSpan.FromMilliseconds(4);
                b.MessageLimit = 100;
                b.SizeLimit = 64 * 1024;
            });

            c.PublisherConfirmation = true;
            //c.UseDefaultClusterConfiguration(configuration);
            //c.UseDefaultSslConfiguration(configuration);
        });

        cfg.ReceiveEndpoint(ClientAddCommandDto.QueueName, c =>
        {
            //c.UseDefaultMessageRetry(retryLimit: 10);
            c.UseInMemoryOutbox(ctx, o => o.ConcurrentMessageDelivery = true);
            c.ConsumerPriority = 5;
            c.PrefetchCount = 32;
            c.ConfigureConsumer<ClientConsumer>(ctx);
        });

        cfg.Message<ClientAddCommandDto>(e => e.SetEntityName(ClientAddCommandDto.EntityName)); 
        cfg.ConfigureEndpoints(ctx);
    });
});

//builder.Services.AddMassTransitHostedService(true);

builder.Services
    .AddOptions<MassTransitHostOptions>()
    .Configure(options =>
    {
        options.WaitUntilStarted = false;
        options.StartTimeout = TimeSpan.FromSeconds(30);
        options.StopTimeout = TimeSpan.FromSeconds(30);
    });

builder.Services
    .AddOpenTelemetry()
    .ConfigureResource(resoure => resoure.AddService(configuration["Telemetry:ServiceName"]!))
    .WithMetrics(metrics =>
    {
        metrics.AddAspNetCoreInstrumentation();
        metrics.AddHttpClientInstrumentation();

        metrics.AddOtlpExporter();
    })
    .WithTracing(tracing =>
    {
        tracing.AddAspNetCoreInstrumentation();
        tracing.AddHttpClientInstrumentation();
        tracing.AddSqlClientInstrumentation(o => o.SetDbStatementForText = true);
        tracing.AddEntityFrameworkCoreInstrumentation(o => o.SetDbStatementForText = true);
        tracing.AddRedisInstrumentation();

        tracing.AddSource(MassTransit.Logging.DiagnosticHeaders.DefaultListenerName);

        tracing.AddOtlpExporter();
        //tracing.AddConsoleExporter();
        //tracing.AddJaegerExporter(j => 
        //{
        //    j.AgentHost = configuration["Telemetry:Jaeger:AgentHost"];
        //    j.AgentPort = configuration.GetValue("Telemetry:Jaeger:AgentPort", 6831);
        //    JaegerExportProtocol protocol = ((!(configuration.GetValue("Telemetry:Jaeger:Protocol", "UdpCompactThrift").ToLowerInvariant() == "udpcompactthrift")) ? JaegerExportProtocol.HttpBinaryThrift : JaegerExportProtocol.UdpCompactThrift);
        //    j.Protocol = protocol;
        //});
        //tracing.AddZipkinExporter(delegate (ZipkinExporterOptions z)
        //{
        //    z.Endpoint = new Uri(configuration["Telemetry:Zipkin:Endpoint"] ?? "https://zipkin-server-name:9411/api/v2/spans");
        //    z.UseShortTraceIds = configuration.GetValue("Telemetry:Zipkin:UseShortTraceIds", defaultValue: false);
        //});
    });

builder.Logging.AddOpenTelemetry(logging => logging.AddOtlpExporter());

builder.Services.Configure<HostOptions>(cfg =>
{
    cfg.ShutdownTimeout = TimeSpan.FromSeconds(35);
    cfg.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.Ignore;
});

builder.Services.AddWindowsService();
builder.Services.AddSwaggerGen();

builder.Services.AddMemoryCache();
builder.Services.AddDistributedMemoryCache();

//var connectionMultiplexer = ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis"));
//builder.Services.AddSingleton(connectionMultiplexer);
//builder.Services.AddStackExchangeRedisCache(options =>
//{
//    options.Configuration = configuration.GetConnectionString("Redis");
//    options.InstanceName = "SampleInstance";
//    options.ConnectionMultiplexerFactory = () => Task.FromResult(connectionMultiplexer);
//});

builder.Services
    .AddSingleton<IRedisConnectionPoolManager>(_ => new RedisConnectionPoolManager(new RedisConfiguration
    {
        ConnectionString = configuration.GetConnectionString("Redis"),
        PoolSize = configuration.GetValue("ConnectionStrings:RedisPoolSize", 10),
        ConnectionSelectionStrategy = ConnectionSelectionStrategy.RoundRobin,
        AbortOnConnectFail = false,
    }))
    .AddScoped(provider =>
    {
        var pool = provider.GetRequiredService<IRedisConnectionPoolManager>();
        return pool.GetConnection().GetDatabase();
    });

builder.Services
    .AddScoped<IIdempotencyKeyReader<HttpRequest>, HttpRequestIdempotencyKeyReader>()
    .AddScoped<IIdempotencySerializer, IdempotencySerializer>()
    //.AddScoped<IIdempotencyRepository, RedisIdempotencyRepositoryWithoutDistributedLock>()
    .AddScoped<IIdempotencyRepository, RedisIdempotencyRepository>()
    .Configure<IdempotencyOptions>(x =>
    {
        x.HeaderName = "X-Idempotency-Key";
        x.TTLInHours = configuration.GetValue<int>("Idempotency:TTLInHours", 24); 
    });

//builder.Host.UseSerilog((ctx, config) => config.ReadFrom.Configuration(ctx.Configuration));
//builder.Services.AddMediatR(b => b.RegisterServicesFromAssembly(typeof(Program).Assembly));
//builder.Services.AddMediatR(b => b.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(Program))!));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler(delegate (IApplicationBuilder errorApp)
    {
        errorApp.Run(async delegate (HttpContext context)
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";
            ErrorResponseDto value = ErrorResponseDto.Begin(HttpStatusCode.InternalServerError, "Falha interna durante o processamento. Tente novamente.");
            await context.Response.WriteAsync(JsonSerializer.Serialize(value, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull }));
        });
    });

    app.UseStatusCodePages(async delegate (StatusCodeContext context)
    {
        ErrorResponseDto value = ErrorResponseDto.Begin((HttpStatusCode)context.HttpContext.Response.StatusCode, ReasonPhrases.GetReasonPhrase(context.HttpContext.Response.StatusCode));
        context.HttpContext.Response.ContentType = "application/json";
        await context.HttpContext.Response.WriteAsync(JsonSerializer.Serialize(value, new JsonSerializerOptions  { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull  }));
    });
}

app.UseHttpsRedirection();
app.UseResponseCompression();
app.UseCors("CorsPolicy");
app.UseAuthorization();
app.UseMiddleware<IdempotencyMiddleware>(Array.Empty<object>());
app.MapControllers();

await using (var localScope = app.Services.CreateAsyncScope())
{
    //Abre conexão com o banco para encher o pool e ganhar performance na primeira request
    await localScope.ServiceProvider.GetRequiredService<IDataContext>().OpenConnection();
}

//app.Run();
await app.RunAsync();
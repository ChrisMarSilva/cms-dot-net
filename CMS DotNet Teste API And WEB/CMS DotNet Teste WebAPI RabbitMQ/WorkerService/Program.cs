using MassTransit;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Repositories.Database;
using RabbitMQ.Worker.Consumers;
using RabbitMQ.Worker.Worker;

var builder = Host.CreateApplicationBuilder(args);

var configuration = builder.Configuration;

var connectionString = configuration.GetConnectionString("Postgres"); // configuration.GetValue<string>("Postgres");

// Configurar PostgreSQL
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(connectionString));

// Configurar MassTransit
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<MessageConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        // var rabbitMqConfig = builder.Configuration.GetSection("RabbitMQ");

        cfg.Host(
            configuration.GetValue("RabbitMQ:Host", "localhost"),
            configuration.GetValue<ushort>("Rabbit:Port", 5672),
            configuration.GetValue("RabbitMQ:VirtualHost", "/"),
            h =>
            {
                h.Username(configuration["RabbitMQ:Username"]!);
                h.Password(configuration["RabbitMQ:Password"]!);
                //h.UseDefaultClusterConfiguration(configuration);
                //h.UseDefaultSslConfiguration(configuration);
                h.PublisherConfirmation = configuration.GetValue("Rabbit:PublisherConfirmation", true);
            });

        cfg.ReceiveEndpoint(configuration["RabbitMQ:Queue"]!, e =>
        {
            e.ConfigureConsumer<MessageConsumer>(context);
            
            // Configurar Retry Policy - 3: Número de tentativas - 5s: Intervalo entre cada tentativa.
            e.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(5)));
            // e.UseMessageRetry(r => r.Exponential(3, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(2)));
            //e.UseMessageRetry(r => r.Immediate(3));

            // Dead Letter Queue (DLQ)
            e.UseDelayedRedelivery(r => r.Intervals(TimeSpan.FromSeconds(10))); // Reenvia mensagens após um intervalo
            e.DiscardSkippedMessages(); // Mensagens falhas são descartadas após retries.

            e.UseInMemoryOutbox(context, o => o.ConcurrentMessageDelivery = true);
            e.ConsumerPriority = 5;
            e.PrefetchCount = 32;
            e.ConfigureConsumeTopology = false;
        });

        //cfg.Message<Fault>(e => e.SetEntityName("jd.fault"));
    });
});

builder.Services
    .AddOptions<MassTransitHostOptions>()
    .Configure(options =>
    {
        options.WaitUntilStarted = true;
        options.StartTimeout = TimeSpan.FromSeconds(10);
    });

// Adicionar Hosted Service
builder.Services.AddHostedService<MassTransitWorker>();

var app = builder.Build();

await using (var serviceScope = app.Services.CreateAsyncScope())
await using (var dbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>())
{
    await dbContext.Database.EnsureCreatedAsync();
}

app.Run();

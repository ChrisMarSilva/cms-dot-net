using MassTransit;
using RabbitMQ.Contratos.Requests;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var configuration = builder.Configuration;

//// Carregar configurações do RabbitMQ
//var rabbitMqConfig = builder.Configuration.GetSection("RabbitMQ");
//var rabbitCfg = configuration.GetSection("RabbitMQ");

//// Validar configurações obrigatórias
//if (string.IsNullOrEmpty(rabbitMqConfig["Host"]) || 
//    string.IsNullOrEmpty(rabbitMqConfig["Port"]) || 
//    string.IsNullOrEmpty(rabbitMqConfig["VirtualHost"]) || 
//    string.IsNullOrEmpty(rabbitMqConfig["Username"]) ||
//    string.IsNullOrEmpty(rabbitMqConfig["Password"]) ||
//    string.IsNullOrEmpty(rabbitMqConfig["Queue"])) 
//    throw new Exception("Configurações do RabbitMQ estão incompletas no appsettings.json");

// builder.Services.AddTransient<IPublishBusExtension, PublishBus>();

// Configurar MassTransit com RabbitMQ
builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter(); // formatar os nomes de fila para Caso Kebab "MyQueue" -> "my-queue"
    //x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter(prefix: "dev", includeNamespace: false));

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(
            configuration.GetValue("RabbitMQ:Host", "localhost"),
            configuration.GetValue<ushort>("RabbitMQ:Port", 5672),
            configuration.GetValue("RabbitMQ:VirtualHost", "/"),
            h =>
        {
            h.Username(configuration["RabbitMQ:Username"]!);
            h.Password(configuration["RabbitMQ:Password"]!);
            h.PublisherConfirmation = configuration.GetValue("RabbitMQ:PublisherConfirmation", true);
        });

        cfg.Message<Fault>(e => e.SetEntityName("queue.fault"));
        cfg.Message<MensagemDto>(e => e.SetEntityName(configuration.GetValue("RabbitMQ:Queue", "queue")));
    });
});

builder.Services
    .AddOptions<MassTransitHostOptions>()
    .Configure(options =>
    {
        options.WaitUntilStarted = true;
        options.StartTimeout = TimeSpan.FromSeconds(10);
        options.StopTimeout = TimeSpan.FromSeconds(10);
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.MapOpenApi();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

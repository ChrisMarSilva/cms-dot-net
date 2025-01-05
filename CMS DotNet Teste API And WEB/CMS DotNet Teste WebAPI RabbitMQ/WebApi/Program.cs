using MassTransit;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var configuration = builder.Configuration;

// WARN[0002] Request Failed
// error="Post \"https://localhost:7222/api/rabbitmq/send\": dial tcp 127.0.0.1:7222:
// connectex: No connection could be made because the target machine actively refused it."

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

// Configurar MassTransit com RabbitMQ
builder.Services.AddMassTransit(config =>
{
    config.UsingRabbitMq((_, cfg) =>
    {
        cfg.Host(
            configuration.GetValue("RabbitMQ:Host", "localhost"),
            configuration.GetValue<ushort>("RabbitMQ:Port", 5672),
            configuration.GetValue("RabbitMQ:VirtualHost", "/"), 
            h =>
        {
            h.Username(configuration["RabbitMQ:Username"]!);
            h.Password(configuration["RabbitMQ:Password"]!);
            //h.UseDefaultClusterConfiguration(configuration);
            //h.UseDefaultSslConfiguration(configuration);
            h.PublisherConfirmation = configuration.GetValue("RabbitMQ:PublisherConfirmation", true);
        });

        // cfg.Message<Fault>(e => e.SetEntityName("jd.fault"));
    });
});

builder.Services
    .AddOptions<MassTransitHostOptions>()
    .Configure(options =>
    {
        options.WaitUntilStarted = false;
        options.StartTimeout = TimeSpan.FromSeconds(10);
    });

var app = builder.Build();
if (app.Environment.IsDevelopment())
    app.MapOpenApi();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

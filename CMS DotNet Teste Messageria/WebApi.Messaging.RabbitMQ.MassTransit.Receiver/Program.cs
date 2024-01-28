using MassTransit;
using WebApi.Messaging.RabbitMQ.MassTransit.Receiver.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();
    x.SetInMemorySagaRepositoryProvider();

    //x.AddConsumer<DriverNotificationConsumerService>();

    // var entryAssembly = Assembly.GetExecutingAssembly();
    // x.AddConsumers(entryAssembly);

    var asb = typeof(Program).Assembly;
    x.AddConsumers(asb);
    x.AddSagaStateMachines(asb);
    x.AddSagas(asb);
    x.AddActivities(asb);

    x.UsingRabbitMq((context, config) =>
    {
        config.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        //config.ReceiveEndpoint("driver-notification", cfg =>
        //{
        //    cfg.Durable = true;
        //    cfg.ConcurrentMessageLimit = 10;
        //    cfg.PrefetchCount = 40;
        //    cfg.ConfigureConsumeTopology = false;
        //    cfg.UseMessageRetry(r => r.Interval(5, 1000));
        //    cfg.ConfigureConsumer<DriverNotificationConsumerService>(context, cfg => { });
        //});

        config.ConfigureEndpoints(context);
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

app.Run();

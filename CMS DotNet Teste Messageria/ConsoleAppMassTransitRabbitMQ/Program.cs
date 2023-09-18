using MassTransit;
using MassTransit.Testing;
using System.Text.Json;


Console.WriteLine("INI");
try
{

    var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
    {
        // cfg.AddConsumer<TextMessageConsumer>();

        // cfg.Host("localhost", 5672, "/", h =>
        cfg.Host(new Uri("rabbitmq://localhost:5672"), h =>
        {
            h.Username("guest");
            h.Password("guest");
            h.ConfigureBatchPublish(b =>
            {
                b.Enabled = true;
                b.Timeout = TimeSpan.FromMilliseconds(4);
                b.MessageLimit = 100;
                b.SizeLimit = 64 * 1024;
            });
            h.PublisherConfirmation = true;
        });

        // cfg.UseRawJsonSerializer();
        // cfg.AutoStart = true;
        // cfg.Publish<IServerNotificationMessage>(e => e.ExchangeType = RabbitMQ.Client.ExchangeType.Direct);

        cfg.ReceiveEndpoint(TextMessageCommand.QueueName, c =>
        {
            //c.UseDefaultMessageRetry(retryLimit: 10);
            c.UseInMemoryOutbox(o => o.ConcurrentMessageDelivery = true);
            c.ConsumerPriority = 5;
            c.PrefetchCount = 32;
            c.UseMessageRetry(r => r.Interval(2, 100));

            c.Consumer<TextMessageConsumer>();
        });

        // cfg.Message<TextMessage>(e => e.SetEntityName(TextMessageCommand.EntityName));
        // cfg.Message<TextMessageCommand>(e => e.SetEntityName(TextMessageCommand.EntityName));
        // cfg.Publish<TextMessageCommand>(p => p.Exclude = true);
        // cfg.Publish<TextMessageCommand>(e => e.ExchangeType = ExchangeType.Direct);
        // cfg.Send<TextMessageCommand>(e => { e.UseRoutingKeyFormatter(context => context.Message.CommandId.ToString()); });
        //cfg.Message<SfnSendCommand>(e => e.SetEntityName(SfnSendCommand.EntityName));
        //cfg.Message<SfnSendCommandResult>(e => e.SetEntityName(SfnSendCommandResult.EntityName));
    });

    bus.Start();

    var message = new TextMessage { Text = "Testing 12345" };
    string jsonMessage = JsonSerializer.Serialize(message);

    var command = new TextMessageCommand(message);
    string jsonCommand = JsonSerializer.Serialize(command);

    //try
    //{
    //    bus.Send<TextMessage>(message).Wait();
    //    Console.WriteLine(" - bus.Send<TextMessage>().Ok");
    //}
    //catch (Exception ex)
    //{
    //    Console.WriteLine($" - bus.Send<TextMessage>().Erro: {ex.Message}");
    //}

    //try
    //{
    //    bus.Publish<TextMessage>(message, x => { x.MessageId = command.CommandId; }).Wait();
    //    Console.WriteLine(" - bus.Publish<TextMessage>().Ok");
    //}
    //catch (Exception ex)
    //{
    //    Console.WriteLine($" - bus.Publish<TextMessage>().Erro: {ex.Message}");
    //}

    //try
    //{
    //    bus.Send(jsonMessage).Wait();
    //    Console.WriteLine(" - bus.Send<jsonMessage>().Ok");
    //}
    //catch (Exception ex)
    //{
    //    Console.WriteLine($" - bus.Send<jsonMessage>().Erro: {ex.Message}");
    //}

    //try
    //{
    //    bus.Publish(jsonMessage, x => { x.MessageId = command.CommandId; }).Wait();
    //    Console.WriteLine(" - bus.Publish<jsonMessage>().Ok");
    //}
    //catch (Exception ex)
    //{
    //    Console.WriteLine($" - bus.Publish<jsonMessage>().Erro: {ex.Message}");
    //}

    //try
    //{
    //    bus.Send<TextMessageCommand>(command).Wait();
    //    Console.WriteLine(" - bus.Send<TextMessageCommand>().Ok");
    //}
    //catch (Exception ex)
    //{
    //    Console.WriteLine($" - bus.Send<TextMessageCommand>().Erro: {ex.Message}");
    //}

    //try
    //{
    //    bus.Publish<TextMessageCommand>(command, x => { x.MessageId = command.CommandId; }).Wait();
    //    Console.WriteLine(" - bus.Publish<TextMessageCommand>().Ok");
    //}
    //catch (Exception ex)
    //{
    //    Console.WriteLine($" - bus.Publish<TextMessageCommand>().Erro: {ex.Message}");
    //}

    //try
    //{
    //    bus.Send(jsonCommand).Wait();
    //    Console.WriteLine(" - bus.Send<jsonCommand>().Ok");
    //}
    //catch (Exception ex)
    //{
    //    Console.WriteLine($" - bus.Send<jsonCommand>().Erro: {ex.Message}");
    //}

    //try
    //{
    //    bus.Publish(jsonCommand, x => { x.MessageId = command.CommandId; }).Wait();
    //    Console.WriteLine(" - bus.Publish<jsonCommand>().Ok");
    //}
    //catch (Exception ex)
    //{
    //    Console.WriteLine($" - bus.Publish<jsonCommand>().Erro: {ex.Message}");
    //}

    var sendEndpoint = bus.GetSendEndpoint(new Uri($"rabbitmq://localhost:5672/{TextMessageCommand.QueueName}")).Result;

    try
    {
        sendEndpoint.Send<TextMessage>(message);
        Console.WriteLine(" - sendEndpoint.Send<TextMessage>().Ok");
    }
    catch (Exception ex)
    {
        Console.WriteLine($" - sendEndpoint.Send<TextMessage>().Erro: {ex.Message}");
    }

    try
    {
        sendEndpoint.Send(jsonMessage);
        Console.WriteLine(" - sendEndpoint.Send<jsonMessage>().Ok");
    }
    catch (Exception ex)
    {
        Console.WriteLine($" - sendEndpoint.Send<jsonMessage>().Erro: {ex.Message}");
    }

    try
    {
        sendEndpoint.Send<TextMessageCommand>(command);
        Console.WriteLine(" - sendEndpoint.Send<TextMessageCommand>().Ok");
    }
    catch (Exception ex)
    {
        Console.WriteLine($" - sendEndpoint.Send<TextMessageCommand>().Erro: {ex.Message}");
    }

    try
    {
        sendEndpoint.Send(jsonCommand);
        Console.WriteLine(" - sendEndpoint.Send<jsonCommand>().Ok");
    }
    catch (Exception ex)
    {
        Console.WriteLine($" - sendEndpoint.Send<jsonCommand>().Erro: {ex.Message}");
    }

    Console.ReadLine();
    bus.Stop();
}
catch (Exception ex)
{
    Console.WriteLine($"ERRO: {ex.Message}");
}
finally
{
    Console.WriteLine("FIM");
    Console.ReadLine();
}


public class TextMessage
{
    public string Text { get; set; } = string.Empty;
}

public record TextMessageCommand(TextMessage message)
{
    #region Configuração Mensageria
    public const string EntityName = "cms.test.message.command.entity";
    public const string QueueName = "cms.test.message.command.queue";
    #endregion

    public Guid CommandId { get; init; } = Guid.NewGuid();
    public DateTime DtHrRequest { get; init; } = DateTime.Now;
}

public class TextMessageConsumer : IConsumer<TextMessage>
{
    public async Task Consume(ConsumeContext<TextMessage> context)
    {
        // var jsonMessage = JsonConvert.SerializeObject(context.Message);
        //  Console.WriteLine($"OrderCreated message: {jsonMessage}");
        Console.WriteLine($"Nova mensagem recebida: {context.Message}");
    }
}
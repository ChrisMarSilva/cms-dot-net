using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

Console.WriteLine("INI");
try
{

    var queueName = "cms.test.message.queue";

    var factory = new ConnectionFactory()
    {
        //Uri = new Uri("rabbitmq://localhost:5672")
        HostName = "localhost",
        Port = 5672,
        VirtualHost = "/",
        UserName = "guest",
        Password = "guest"
    };

    using var connection = factory.CreateConnection();
    using var channel = connection.CreateModel();

    channel.QueueDeclare(
        queue: queueName,
        durable: true,
        exclusive: false,
        autoDelete: false,
        arguments: null);

    channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

    var properties = channel.CreateBasicProperties();
    properties.Persistent = true;

    for (int i = 0; i < 100; i++)
    {
        var message = new TextMessage(Guid.NewGuid(), $"Testing #{i}", DateTime.Now);
        var jsonMessage = JsonSerializer.Serialize(message);
        var bytesMessage = Encoding.UTF8.GetBytes(jsonMessage);

        channel.BasicPublish(
            exchange: string.Empty,
            routingKey: queueName,
            //basicProperties: null,
            basicProperties: properties,
            body: bytesMessage);

        // Console.WriteLine($"[Mensagem enviada] {jsonMessage}");
    }

    var consumer = new EventingBasicConsumer(channel);
    //consumer.Received += Consumer_Received;

    consumer.Received += (model, ea) =>
    {
        var bytesMessage = ea.Body.ToArray();
        var jsonMessage = Encoding.UTF8.GetString(bytesMessage);
        var message = JsonSerializer.Deserialize<TextMessage>(jsonMessage);

        Console.WriteLine($" [x] Received {message}");

        //channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
    };

    channel.BasicConsume(
        queue: queueName,
        autoAck: true,
        consumer: consumer);

    //while (true)
    //{
    //    Console.WriteLine($"Worker ativo em: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
    //    Thread.Sleep(1000 * 1); //1 sec
    //}

    //Console.WriteLine(" Press [enter] to exit.");
    //Console.ReadLine();

}
catch (Exception ex)
{
    Console.WriteLine($"ERRO: {ex.GetType().FullName} | {ex.Message}");
}
finally
{
    Console.WriteLine("FIM");
    Console.ReadLine();
}

public record TextMessage(Guid Id, string Text, DateTime DtHr);

//public void Consumer_Received() // object sender, BasicDeliverEventArgs e
//{
//    /// Console.WriteLine( $"[Nova mensagem | {DateTime.Now:yyyy-MM-dd HH:mm:ss}] " +Encoding.UTF8.GetString(e.Body.ToArray()));
//}


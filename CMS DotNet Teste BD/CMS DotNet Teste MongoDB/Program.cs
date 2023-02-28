using MongoDB.Bson;
using MongoDB.Driver;
using RabbitMQ.Client;
using System.Xml.Linq;
using System;

Console.WriteLine("Inicio"); 
try
{

    var connectionString = "mongodb+srv://<username>:<password>@<cluster-address>/test?w=majority";
    var client = new MongoClient(connectionString=connectionString);
    var database = client.GetDatabase("test");

    // var settings = MongoClientSettings.FromConnectionString("<connection string>");
    // settings.ServerApi = new ServerApi(ServerApiVersion.V1);
    //var client = new MongoClient(settings);

    // GET e PUT
    var factory = new ConnectionFactory() { HostName = "localhost" };
    using (var connection = factory.CreateConnection())
    using (var channel = connection.CreateModel())
    {
        channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);
        string message = "Hello World!";
        var body = System.Text.Encoding.UTF8.GetBytes(message);
        channel.BasicPublish(exchange: "", routingKey: "hello", basicProperties: null, body: body);
        Console.WriteLine(" [x] Sent {0}", message);
    }

}
catch (Exception err)
{
    Console.WriteLine($"Erro: {err.Message}");
}
finally
{
    Console.WriteLine("Fim");
    Console.ReadLine();
}
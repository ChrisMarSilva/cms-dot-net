namespace ConsoleAppRabbitMQManager;

public class QueueInfo
{
    public string Name { get; set; }
    public int Messages { get; set; }
    public string Type { get; set; } // classic // stream
}

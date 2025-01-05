namespace RabbitMQ.Models.Entities;

public class Message
{
    private Message()
    {
        Id = Guid.NewGuid();
        DtHrRegistro = DateTime.UtcNow;
    }

    public Message(string texto) : this()
    {
        Texto = texto;
    }

    public Guid Id { get; set; }
    public string Texto { get; set; } = string.Empty;
    public DateTime DtHrRegistro { get; set; }
}

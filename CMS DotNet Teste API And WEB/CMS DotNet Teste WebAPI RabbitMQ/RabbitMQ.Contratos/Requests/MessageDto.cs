namespace RabbitMQ.Contratos.Requests;

public record MensagemDto(
    string? IdMsgJdPi,
    string? IdMsg,
    string? TpMsg,
    string? QueueMsg,
    string? XmlMsg);

// public record MessageDto(string Text);

//public class MessageDto
//{
//    public string Text { get; set; } = string.Empty;
//}

namespace Cache.Web.Contracts;

public record MensagemDto(
    string? IdMsgJdPi,
    string? IdMsg,
    string? TpMsg,
    string? QueueMsg,
    string? XmlMsg);
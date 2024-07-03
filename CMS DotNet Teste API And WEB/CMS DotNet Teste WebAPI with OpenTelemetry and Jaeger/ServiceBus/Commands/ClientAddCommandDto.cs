using Project.Domain.Dtos.Request;

namespace Project.ServiceBus.Commands;

public record ClientAddCommandDto(string IdempotenceKey, ClientRequestDto Request)
{
    public Guid CommandId { get; init; } = Guid.NewGuid();
    public DateTime DtHrRequest { get; init; } = DateTime.Now;

    public const string EntityName = "project.client.add.entity";
    public const string QueueName = "project.client.add.queue";
}
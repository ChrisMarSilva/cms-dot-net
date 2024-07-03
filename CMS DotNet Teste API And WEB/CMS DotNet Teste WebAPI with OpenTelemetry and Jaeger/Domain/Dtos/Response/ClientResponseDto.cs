using System.ComponentModel.DataAnnotations;

namespace Project.Domain.Dtos.Response;

public record ClientResponseDto
{
    public ClientResponseDto(string idempotenceKey, Guid commandId, DateTime dtHrRequest)
    {
        IdempotenceKey = idempotenceKey;
        CommandId = commandId;
        DtHrRequest = dtHrRequest;
    }

    [Required] public string IdempotenceKey { get; init; }
    [Required] public Guid CommandId { get; init; }
    [Required] public DateTime DtHrRequest { get; init; }
}

namespace Project.Domain.Dtos.Response;

public record ProductResponseDto(
    int Id,
    string Name,
    decimal Price,
    DateTime CreatedAt,
    DateTime? UpdatedAt);
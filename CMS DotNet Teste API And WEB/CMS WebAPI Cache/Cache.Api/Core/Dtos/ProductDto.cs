namespace Cache.Api.Core.Dtos;

public record ProductDto(
    Guid Id, string 
    Name, string 
    Description, 
    decimal Price);
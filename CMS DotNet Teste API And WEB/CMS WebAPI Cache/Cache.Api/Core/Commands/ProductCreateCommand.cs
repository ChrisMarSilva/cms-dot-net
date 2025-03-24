using MediatR;

namespace Cache.Api.Core.Commands;

public record ProductCreateCommand(
    string Name, 
    string Description, 
    decimal Price) : IRequest<Guid>;
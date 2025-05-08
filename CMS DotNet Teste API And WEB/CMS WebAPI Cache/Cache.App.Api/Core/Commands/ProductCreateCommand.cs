using MediatR;

namespace Cache.Shared.Core.Commands;

public record ProductCreateCommand(
    string Name,
    string Description,
    decimal Price) : IRequest<Guid>;
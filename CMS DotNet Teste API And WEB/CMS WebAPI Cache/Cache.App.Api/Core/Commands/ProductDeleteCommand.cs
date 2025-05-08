using MediatR;

namespace Cache.Shared.Core.Commands;

public record ProductDeleteCommand(
    Guid Id) : IRequest;

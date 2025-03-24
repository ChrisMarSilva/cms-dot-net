using MediatR;

namespace Cache.Api.Core.Commands;

public record ProductDeleteCommand(Guid Id) : IRequest;

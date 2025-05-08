using MediatR;

using Cache.Shared.Core.Dtos;

namespace Cache.Shared.Core.Queries;

public record ProductGetQuery(
    Guid Id) : IRequest<ProductDto>;
using Cache.Api.Core.Dtos;
using MediatR;

namespace Cache.Api.Core.Queries;

public record ProductGetQuery(
    Guid Id) : IRequest<ProductDto>;
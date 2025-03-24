using Cache.Api.Core.Dtos;
using MediatR;

namespace Cache.Api.Core.Queries;

public record ProductListQuery : IRequest<List<ProductDto>>;
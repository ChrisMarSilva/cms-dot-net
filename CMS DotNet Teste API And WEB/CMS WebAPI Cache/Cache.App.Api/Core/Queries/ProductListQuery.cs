using Cache.Shared.Core.Dtos;
using MediatR;

namespace Cache.Shared.Core.Queries;

public record ProductListQuery : IRequest<List<ProductDto>>;
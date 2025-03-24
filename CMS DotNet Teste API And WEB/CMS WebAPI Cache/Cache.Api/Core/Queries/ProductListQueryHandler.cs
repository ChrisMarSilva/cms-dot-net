using Cache.Api.Core.Dtos;
using Cache.Api.Database.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cache.Api.Core.Queries;

public class ProductListQueryHandler(AppDbContext context) : IRequestHandler<ProductListQuery, List<ProductDto>>
{
    public async Task<List<ProductDto>> Handle(ProductListQuery request, CancellationToken cancellationToken) =>
        await context.Products
            .Select(p => new ProductDto(p.Id, p.Name, p.Description, p.Price))
            .ToListAsync();
}

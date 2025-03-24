using Cache.Api.Core.Dtos;
using Cache.Api.Database.Contexts;
using MediatR;

namespace Cache.Api.Core.Queries;

public class ProductGetQueryHandler(AppDbContext context) : IRequestHandler<ProductGetQuery, ProductDto?>
{
    public async Task<ProductDto?> Handle(ProductGetQuery request, CancellationToken cancellationToken)
    {
        var product = await context.Products.FindAsync(request.Id);

        if (product == null)
            return null;

        return new ProductDto(product.Id, product.Name, product.Description, product.Price);
    }
}

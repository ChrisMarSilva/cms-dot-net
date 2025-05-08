using MediatR;

namespace Cache.Shared.Core.Commands;

//public class ProductDeleteCommandHandler(AppDbContext context) : IRequestHandler<ProductDeleteCommand>
//{
//    public async Task Handle(ProductDeleteCommand request, CancellationToken cancellationToken)
//    {
//        var product = await context.Products.FindAsync(request.Id);

//        if (product == null)
//            return;

//        context.Products.Remove(product);
//        await context.SaveChangesAsync();
//    }
//}
namespace Cache.Shared.Core.Commands;

//public class ProductCreateCommandHandler(AppDbContext context) : IRequestHandler<ProductCreateCommand, Guid>
//{
//    public async Task<Guid> Handle(ProductCreateCommand command, CancellationToken cancellationToken)
//    {
//        var product = new ProductModel(command.Name, command.Description, command.Price);

//        await context.Products.AddAsync(product);
//        await context.SaveChangesAsync();

//        return product.Id;
//    }
//}
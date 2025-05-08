namespace Cache.Domain.Models;

public class ProductModel
{
    private ProductModel()
    {

    }

    public ProductModel(string name, string description, decimal price)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        Price = price;
    }

    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
}
namespace Project.Domain.Models;

public sealed class ProductModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public ProductModel(string name, decimal price) 
    {
        Name = name;
        Price = price;
        CreatedAt = DateTime.Now;
    }

    public void Update(string name, decimal price)
    {
        Name = name;
        Price = price;
        UpdatedAt = DateTime.Now;
    }
}

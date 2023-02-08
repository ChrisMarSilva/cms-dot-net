namespace IWantApp.Domain.Products;

public class Product : Entity
{
    public string Name { get; private set; }
    public Guid CategoryId { get; private set; }
    public Category Category { get; private set; }
    public string Description { get; private set; }
    public bool IsStock { get; private set; }
    public bool Active { get; private set; } = true;
    public decimal Price { get; private set; }    
    public ICollection<Order> Orders { get; private set; }

    private Product() { }

    public Product(string name, Category category, string description, bool isStock, decimal price, string createdBy)
    {
        this.Name = name;
        this.Category = category;
        this.Description = description;
        this.IsStock = isStock;
        this.Price = price;
        this.Active = true;
        this.CreatedBy = createdBy;
        this.CreatedOn = DateTime.Now;
        this.EditedBy = createdBy;
        this.EditedOn = DateTime.Now;

        this.Validate();
    }

    public void EditInfo(string name, Category category, string description, bool isStock, decimal price, bool active, string editedBy)
    {
        this.Name = name;
        this.Category = category;
        this.Description = description;
        this.IsStock = isStock;
        this.Price = price;
        this.Active = active;
        this.EditedBy = editedBy;
        this.EditedOn = DateTime.Now;

        this.Validate();
    }

    private void Validate()
    {
        var contract = new Contract<Product>()
            .IsNotNullOrEmpty(Name, "Name")
            .IsGreaterOrEqualsThan(Name, 3, "Name")
            .IsNotNull(Category, "Category", "Category not found")
            .IsNotNullOrEmpty(Description, "Description")
            .IsGreaterOrEqualsThan(Description, 3, "Description")
            .IsGreaterOrEqualsThan(Price, 1, "Price")
            .IsNotNullOrEmpty(CreatedBy, "CreatedBy")
            .IsNotNullOrEmpty(EditedBy, "EditedBy");

        this.AddNotifications(contract);
    }
}

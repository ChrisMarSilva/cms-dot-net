namespace Project.Domain.Models;

public sealed class ClientModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public ClientModel(string name)
    {
        Name = name;
        CreatedAt = DateTime.Now;
    }

    public void Update(string name)
    {
        Name = name;
        UpdatedAt = DateTime.Now;
    }
}

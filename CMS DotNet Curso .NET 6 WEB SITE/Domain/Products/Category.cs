namespace IWantApp.Domain.Products;

public class Category : Entity
{
    public string Name { get; private set; }
    public bool Active { get; private set; } = true;

    private Category() { }

    public Category(string name, string createdBy, string editedBy)
    {
        this.Name = name;
        this.Active = true;
        this.CreatedBy = createdBy;
        this.CreatedOn = DateTime.Now;
        this.EditedBy = editedBy;
        this.EditedOn = DateTime.Now;

        this.Validate();
    }

    public void EditInfo(string name, bool active, string editedBy)
    {
        this.Active = active;
        this.Name = name;
        this.EditedBy = editedBy;
        this.EditedOn = DateTime.Now;

        this.Validate();
    }

    private void Validate()
    {
        var contract = new Contract<Category>()
            .IsNotNullOrEmpty(this.Name, "Name", "Nome � obrigat�rio")
            .IsGreaterOrEqualsThan(this.Name, 3, "Name", "Nome muito pequeno")
            .IsNotNullOrEmpty(this.CreatedBy, "CreatedBy", "Usu�rio de Cria��o � obrigat�rio")
            .IsNotNullOrEmpty(this.EditedBy, "EditedBy", "Usu�rio de Edi��o � obrigat�rio");

        this.AddNotifications(contract);
    }
}
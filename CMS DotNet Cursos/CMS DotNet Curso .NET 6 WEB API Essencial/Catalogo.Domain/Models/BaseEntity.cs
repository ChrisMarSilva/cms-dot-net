namespace Catalogo.Domain.Models;

public class BaseEntity
{
    public Guid Id { get; private set; }
    public DateTime DataCadastro { get; private set; } // CreatedAt // CreateDate
    public DateTime? DataAlteracao { get; private set; } // UpdatedAt // ModifiedDate
    //public bool IsActive { get; set; }

    public BaseEntity()
    {
        this.Id = Guid.NewGuid();
        this.DataCadastro = DateTime.UtcNow;
        this.DataAlteracao = null;
    }
}

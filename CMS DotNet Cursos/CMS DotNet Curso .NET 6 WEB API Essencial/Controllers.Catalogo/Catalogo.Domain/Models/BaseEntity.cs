using System.ComponentModel.DataAnnotations;

namespace Catalogo.Domain.Models;

public class BaseEntity
{

    [Key]
    public Guid Id { get; private set; } // Id // ID
    public DateTime DataCadastro { get; private set; } // CreatedAt // CreateDate // DataCadastro
    public DateTime? DataAlteracao { get; private set; } // UpdatedAt // ModifiedDate // DataAlteracao
    //public bool IsActive { get; set; }

    public BaseEntity()
    {
        this.Id = Guid.NewGuid();
        this.DataCadastro = DateTime.UtcNow;
        this.DataAlteracao = null;
    }

    public void Update()
    {
        this.DataAlteracao = DateTime.UtcNow;
    }
}

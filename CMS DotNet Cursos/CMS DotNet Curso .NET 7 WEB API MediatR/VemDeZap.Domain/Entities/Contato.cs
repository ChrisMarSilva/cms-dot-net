using VemDeZap.Domain.Entities.Base;

namespace VemDeZap.Domain.Entities;

public class Contato // : EntityBase
{
    protected Contato()
    {

    }

    public Guid Id { get; set; }
    public Usuario Usuario { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public int Nicho { get; set; }
}

using VemDeZap.Domain.Entities.Base;

namespace VemDeZap.Domain.Entities;

public class Campanha // : EntityBase
{
    protected Campanha()
    {

    }

    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public Usuario Usuario { get; set; }
}

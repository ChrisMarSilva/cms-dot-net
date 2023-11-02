using VemDeZap.Domain.Entities.Base;

namespace VemDeZap.Domain.Entities;

public class Envio // : EntityBase
{
    protected Envio()
    {

    }

    public Guid Id { get; set; }
    public Campanha Campanha { get; set; }
    public Grupo Grupo { get; set; }
    public Contato Contato { get; set; }
    public bool Enviado { get; set; }
}

using VemDeZap.Domain.Entities.Base;

namespace VemDeZap.Domain.Entities;

public class Grupo // : EntityBase
{
    protected Grupo()
    {

    }

    public Grupo(Usuario usuario, string nome, int nicho)
    {
        Usuario = usuario;
        Nome = nome;
        Nicho = nicho;

        //if (usuario == null)
        //    AddNotification("Usuario", "Informe o usuário");

        //new AddNotifications<Grupo>(this)
        //    .IfNullOrInvalidLength(x => x.Nome, 3, 150)
        //    .IfEnumInvalid(x => x.Nicho);
    }

    public Guid Id { get; set; }
    public Usuario Usuario { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Nicho { get; set; }

    public void AlterarGrupo(string nome, int nicho)
    {
        Nome = nome;
        Nicho = nicho;
    }
}

namespace CMS_DotNet_Teste_WebAPI_with_Redis.Models;

public class Produto
{
    public Produto()
    {
        Id = Guid.NewGuid();
    }

    public Produto(string nome) : this()
    {
        Nome = nome;
    }

    public Guid Id { get; set; }
    public String Nome { get; set; } = string.Empty;

    public void Update(string nome)
    {
        Nome = nome;
    }
}

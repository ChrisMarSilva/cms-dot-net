using System.ComponentModel.DataAnnotations;

namespace Catalogo.Domain.Models;

public partial class Aluno : BaseEntity
{
    [Required]
    [StringLength(80, ErrorMessage = "")]
    public string Nome { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(100, ErrorMessage = "")]
    public string Email { get; set; }

    [Required]
    public int Idade { get; set; }

    public Aluno() : base() { }

    public Aluno(string nome, string email, int idade) : this()
    {
        Nome = nome;
        Email = email;
        Idade = idade;
    }

    public void Update(string nome, string email, int idade)
    {
        base.Update();

        Nome = nome;
        Email = email;
        Idade = idade;
    }
}

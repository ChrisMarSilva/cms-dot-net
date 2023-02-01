public class Delphi
{
    public string Nome { get; set; }
    public int Idade { get; set; }

    public Delphi(string nome, int idade)
    {
        Nome = nome;
        Idade = idade;
    }

    public void MostrarDados()
    {
        Console.WriteLine($"Nome: {Nome}");
        Console.WriteLine($"Idade: {Idade}");
    }
}
using MongoDB.Bson;

namespace ConsoleAppMongoDB;

public class Pessoa
{
    public ObjectId Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Idade { get; set; }
    public DateTime DataCadastro { get; set; }
}
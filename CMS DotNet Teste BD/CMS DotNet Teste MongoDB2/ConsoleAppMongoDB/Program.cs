using ConsoleAppMongoDB;
using MongoDB.Bson;
using MongoDB.Driver;

Console.WriteLine("INI");
try
{
    //var connectionString = "mongodb://localhost:27017";
    //var connectionString = "mongodb+srv://<username>:<password>@cluster0.mongodb.net/<dbname>?retryWrites=true&w=majority";
    var connectionString = "mongodb+srv://chrismarsilva:mgYF2bnLRKIVkop3@cluster0.ip353.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";

    var client = new MongoClient(connectionString);
    var database = client.GetDatabase("databaseJd");
    var collection = database.GetCollection<BsonDocument>("collectionJd");

    //// Criar um documento
    //var document = new BsonDocument { { "nome", "Exemplo" }, { "idade", 25 }, { "dataCadastro", DateTime.Now } };
    //collection.InsertOne(document);

    //// Consultar documentos
    //var filter = Builders<BsonDocument>.Filter.Eq("nome", "Exemplo");
    //var resultado = collection.Find(filter).FirstOrDefault();
    //Console.WriteLine(resultado.ToString());

    //// Atualizar documentos
    //var filter = Builders<BsonDocument>.Filter.Eq("nome", "Exemplo");
    //var update = Builders<BsonDocument>.Update.Set("idade", 30);
    //collection.UpdateOne(filter, update);

    //// Remover documentos
    //var filter = Builders<BsonDocument>.Filter.Eq("nome", "Exemplo");
    //collection.DeleteOne(filter);

    //// Inserir usando POCO
    //var collectionPessoa = database.GetCollection<Pessoa>("pessoas");
    //var pessoa = new Pessoa { Nome = "João", Idade = 29, DataCadastro = DateTime.Now };
    //collectionPessoa.InsertOne(pessoa);

}
catch (Exception ex)
{
    Console.WriteLine($"ERRO: {ex.Message}");
    Console.WriteLine($"DETAIL: {ex.StackTrace}");
}
finally
{
    Console.WriteLine("FIM");
    Console.ReadKey();
}
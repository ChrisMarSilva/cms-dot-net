using ConsoleAppMongoDB;
using MongoDB.Bson;
using MongoDB.Driver;

Console.WriteLine("INI");
try
{
    //var connectionString = "mongodb+srv://<username>:<password>@cluster0.mongodb.net/<dbname>?retryWrites=true&w=majority";
    //var connectionString = "mongodb+srv://chrismarsilva:mgYF2bnLRKIVkop3@cluster0.ip353.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";
    //var connectionString = "mongodb://<usuario>:<senha>@<host>:<porta>/<banco_de_dados>?authSource=<banco_de_autenticacao>";
    var connectionString = "mongodb://root:example@localhost:27017";

    var client = new MongoClient(connectionString);
    var database = client.GetDatabase("databaseJd");
    var collection = database.GetCollection<BsonDocument>("collectionJd");

    //BD           MongoDB
    //BaseDeDados  BaseDeDados
    //Tabela       Coleção
    //Coluna       Atributo
    //Registro     Documento

    //// Criar um documento
    //var document = new BsonDocument { { "nome", "Exemplo" }, { "idade", 25 }, { "dataCadastro", DateTime.Now } };
    //collection.InsertOne(document);

    //// Consultar documentos
    //var filterFind = Builders<BsonDocument>.Filter.Eq("nome", "Exemplo");
    //var resultado = collection.Find(filterFind).FirstOrDefault();
    //Console.WriteLine(resultado.ToString());

    //// Atualizar documentos
    //var filterUpdate = Builders<BsonDocument>.Filter.Eq("nome", "Exemplo");
    //var update = Builders<BsonDocument>.Update.Set("idade", 30);
    //collection.UpdateOne(filterUpdate, update);

    //// Remover documentos
    //var filterDelete = Builders<BsonDocument>.Filter.Eq("nome", "Exemplo");
    //collection.DeleteOne(filterDelete);

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
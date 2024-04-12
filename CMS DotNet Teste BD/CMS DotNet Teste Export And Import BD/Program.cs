using System;
using System.Data;
using System.Data.SqlClient;

Console.WriteLine("INI"); 
try
{

    Console.WriteLine("Conectando ao BD de Origem...");
    var tabelaOrigem = "TbDadosOrigem";
    var connectionStringOrigem = @"Data Source=127.0.0.1,1433;Initial Catalog=BDOrigem;User ID=sa;Password=Hello123#;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
    //}
    using var connOrigem = new SqlConnection(connectionStringOrigem);
    connOrigem.Open();

    Console.WriteLine("Conectando ao BD de Destino...");
    var tabelaDestino = "TbDadosDestino";
    var connectionStringDestino = @"Data Source=127.0.0.1,1433;Initial Catalog=BDDestino;User ID=sa;Password=Hello123#;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
    using var connDestino = new SqlConnection(connectionStringDestino);
    connDestino.Open();

    Console.WriteLine("Limpando dados da BD de Destino...");
    string selectDestino = $"TRUNCATE TABLE {tabelaDestino}";
    var commandDelDestino = new SqlCommand(selectDestino, connDestino);
    commandDelDestino.ExecuteNonQuery();

    Console.WriteLine("Consultando dados da BD de Origem...");
    string selectOrigem = $"SELECT * FROM {tabelaOrigem}";
    using var commandSelOrigem = new SqlCommand(selectOrigem, connOrigem);
    using var readerOrigem = commandSelOrigem.ExecuteReader();

    Console.WriteLine("Migrando dados da BD de Origem para o BD de Destino...");
    //var options = SqlBulkCopyOptions.TableLock | SqlBulkCopyOptions.UseInternalTransaction | SqlBulkCopyOptions.KeepIdentity;
    using var bulkCopy = new SqlBulkCopy(connDestino); // new SqlBulkCopy(connectionStringDestino, options)
    bulkCopy.DestinationTableName = tabelaDestino;
    bulkCopy.WriteToServer(readerOrigem);
    
    Console.WriteLine("Dados migrados com sucesso!");
}
catch (Exception ex)
{
    Console.WriteLine($"ERRO: ${ex.Message}");
} 
finally
{
    Console.WriteLine("FIM");
}
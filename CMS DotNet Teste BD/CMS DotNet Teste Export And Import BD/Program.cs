using System.Data.SqlClient;

Console.WriteLine("INI"); 
try
{

    Console.WriteLine("Conectando ao BD de Origem...");
    var tabelaOrigem = "TBJDNPCBNF_CIP_CNVN"; //TbDadosOrigem // TBTESTE // TBJDNPCBNF_CIP_BNF // TBJDNPCBNF_CIP_CNVN // TBJDNPCBNF_CIP_RPRSNTNT
    var connectionStringOrigem = "";
    //connectionStringOrigem = "Data Source=127.0.0.1,1433;Initial Catalog=BDOrigem;User ID=sa;Password=Hello123#;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"; // CMS - NOTE
    connectionStringOrigem = "Data Source=10.0.27.143;Initial Catalog=Teste_JDNPC_CORA_PRD;User ID=Teste_JDNPC_CORA_PRD;Password=Teste_JDNPC_CORA_PRD;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"; // JD - JDNPC_CORA_PRD
    using var connOrigem = new SqlConnection(connectionStringOrigem);
    connOrigem.Open();

    Console.WriteLine("Conectando ao BD de Destino...");
    var tabelaDestino = "TBJDNPCBNF_CIP_CNVN"; // TbDadosDestino // TBTESTE // TBJDNPCBNF_CIP_BNF // TBJDNPCBNF_CIP_CNVN // TBJDNPCBNF_CIP_RPRSNTNT
    var connectionStringDestino = "";
    //connectionStringDestino = "Data Source=127.0.0.1,1433;Initial Catalog=BDDestino;User ID=sa;Password=Hello123#;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"; // CMS - NOTE
    connectionStringDestino = "Data Source=jdsp118;Initial Catalog=Teste_JDNPC_CORA_PRD;User ID=jddesenv;Password=jddesenv;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"; // JD - JDNPC_BNF_DOTNET
    using var connDestino = new SqlConnection(connectionStringDestino);
    connDestino.Open();

    var transactionDestino = connDestino.BeginTransaction();
    try
    {
        //Console.WriteLine("Limpando dados da BD de Destino...");
        //var selectDestino = $"TRUNCATE TABLE {tabelaDestino}";
        //var commandDelDestino = new SqlCommand(selectDestino, connDestino, transactionDestino);
        //commandDelDestino.ExecuteNonQuery();

        Console.WriteLine("Consultando dados da BD de Origem...");
        var selectOrigem = $"SELECT * FROM {tabelaOrigem} WITH(NOLOCK)";
        using var commandSelOrigem = new SqlCommand(selectOrigem, connOrigem);
        using var readerOrigem = commandSelOrigem.ExecuteReader();

        Console.WriteLine("Migrando dados da BD de Origem para o BD de Destino...");
        var optionsDestino = SqlBulkCopyOptions.TableLock | SqlBulkCopyOptions.KeepIdentity | SqlBulkCopyOptions.KeepNulls; // | SqlBulkCopyOptions.UseInternalTransaction
        using var bulkCopy = new SqlBulkCopy(connDestino, optionsDestino, transactionDestino);
        bulkCopy.DestinationTableName = tabelaDestino;
        bulkCopy.BulkCopyTimeout = 1000 * 60 * 30;
        bulkCopy.BatchSize = 10_000;
        //bulkCopy.InsertIfNotExists = false;
        //bulkCopy.Transaction = transaction;
        //bulkCopy.ErrorMode = ErrorModeType.ThrowException;
        //bulkCopy.Log += Console.WriteLine;
        bulkCopy.WriteToServer(readerOrigem);

        transactionDestino.Commit();
        Console.WriteLine("Dados migrados com sucesso!");
    }
    catch (Exception ex)
    {
        transactionDestino.Rollback();
        Console.WriteLine($"ERRO-1: ${ex.Message}");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"ERRO-2: ${ex.Message}");
} 
finally
{
    Console.WriteLine("FIM");
}
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TesteBenchmarkDotNet.Benchmarks;
using TesteBenchmarkDotNet.Data;
using TesteBenchmarkDotNet.Models;

// https://nishanc.medium.com/writing-better-performant-queries-with-linq-on-ef-core-6-0-%EF%B8%8F-85a1a406879

try
{
    BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
    BenchmarkRunner.Run<BenchmarkSelectWhere>(); // BenchmarkSelectWhere // BenchmarkOrderByWhere // BenchmarkMultipleInclude // BenchmarkUnnecessaryJoins

    //using System.Data.SQLite;
    //var connectionString = @"Data Source=C:\Users\chris\AppData\Roaming\DBeaverData\workspace6\.metadata\sample-database-sqlite-1\Chinook.db;Version=3;";
    //using (var conn = new SQLiteConnection(connectionString))

    //using MySql.Data.MySqlClient;
    //var connectionString = "Server=localhost;port=3306;Database=rinha_backend_2024;Uid=root;Pwd=Chrs8723;";
    //using (var conn = new MySqlConnection(connectionString))
    //{
    //    conn.Open();

    //    if (conn.State != System.Data.ConnectionState.Open)
    //    {
    //        Console.WriteLine("Não foi possível estabelecer conexão com o banco de dados.");
    //        return;
    //    }

    //    //var createTableQuery = @"CREATE TABLE IF NOT EXISTS Usuarios ( Id INTEGER PRIMARY KEY AUTOINCREMENT,  Nome TEXT, Idade INTEGER )"; // SQLite
    //    //using (var command = new SQLiteCommand(createTableQuery, conn))
    //    //var createTableQuery = @"CREATE TABLE IF NOT EXISTS Usuarios ( Id INT AUTO_INCREMENT PRIMARY KEY, Nome VARCHAR(255), Idade INT )"; //MySql
    //    //using (var command = new MySqlCommand(createTableQuery, conn))
    //    //{
    //    //    command.ExecuteNonQuery();
    //    //}

    //    //string insertDataQuery = "INSERT INTO Usuarios (Nome, Idade) VALUES ('João', 30)";
    //    //using (var command = new SQLiteCommand(insertDataQuery, conn))
    //    //using (var command = new MySqlCommand(insertDataQuery, conn))
    //    //{
    //    //    command.ExecuteNonQuery();
    //    //}

    //    string selectDataQuery = "SELECT * FROM Cliente"; 
    //    //using (var command = new SQLiteCommand(selectDataQuery, conn))
    //    using (var command = new MySqlCommand(selectDataQuery, conn))
    //    {
    //        using (var reader = command.ExecuteReader())
    //        {
    //            while (reader.Read())
    //            {
    //                // Console.WriteLine($"ID: {reader["Id"]}, Nome: {reader["Nome"]}, Idade: {reader["Idade"]}");
    //                //Console.WriteLine($"ID: {reader["AlbumId"]}, Title: {reader["Title"]}");
    //                Console.WriteLine($"Id: {reader["Id"]}, Limite: {reader["Limite"]}");
    //            }
    //        }
    //    }
    //}

    //short _idCliente = 1;
    //long _limite = 0;
    //string _rawSqlEFLimite = @"SELECT limite FROM Cliente WITH(NOLOCK) WHERE idcliente = {0}";
    //string _rawSqlEFLimite = "SELECT limite as Value FROM Cliente WITH(NOLOCK) WHERE IdCliente = @idCliente";
    //string _rawSqlDPLimite = @"SELECT limite FROM Cliente WITH(NOLOCK) WHERE idcliente = @pIdcliente";
    //ClienteModel cliente = default;
    //string _rawSqlEFFull = @"SELECT * FROM Cliente WITH(NOLOCK) WHERE idcliente = {0}";
    //string _rawSqlDPFull = @"SELECT * FROM Cliente WITH(NOLOCK) WHERE idcliente = @pIdcliente";

    //using (var context = new DataContextSQLServer()) // DataContextSQLite // DataContextMySQL // DataContextSQLServer
    //{
    //    //context.Database.EnsureCreated();

    //    //    foreach (var cliente in context.Clientes)
    //    //        Console.WriteLine($"ID: {cliente.Id}, Limite: {cliente.Limite}");

    //    //_limite = context.Clientes.AsNoTracking().Where(x => x.IdCliente == _idCliente).Select(x => x.Limite).FirstOrDefault();
    //    //Console.WriteLine($"SQLServer_EF_WhereFirst_Limite: {_limite}");

    //    //_limite = (long)context.Database.SqlQueryRaw<long>(sql: _rawSqlEFLimite, parameters: new SqlParameter("@idCliente", _idCliente)).FirstOrDefault();
    //    //Console.WriteLine($"SQLServer_EF_Select_Limite: {_limite}");

    //    //cliente = context.Clientes.FromSqlRaw(_rawSqlEFFull, _idCliente).AsNoTracking().FirstOrDefault();
    //    //Console.WriteLine($"SQLServer_EF_Select_Full: {cliente}");

    //    cliente = context.Clientes.AsNoTracking().Where(x => x.IdCliente == _idCliente).Select(x => new ClienteModel(x.IdCliente, x.Limite)).FirstOrDefault();
    //    Console.WriteLine($"SQLServer_EF_WhereFirst_Full: {cliente}");

    //    cliente = context.Clientes.AsNoTracking().AsEnumerable().Select(x => new ClienteModel(x.IdCliente, x.Limite)).Where(x => x.IdCliente == _idCliente).ToList().FirstOrDefault();
    //    Console.WriteLine($"SQLServer_EF_SelectFirst_Full: {cliente}");

    //    //using (var connection = new SqlConnection(@"Data Source=127.0.0.1,5402;Initial Catalog=RinhaBackend2024;User ID=sa;Password=Hello123#;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
    //    //using (var connection = context.Database.GetDbConnection() as SqlConnection)
    //    //using (var connection = context.GetSqlConnection())
    //    using (var connection = (SqlConnection)context.Database.GetDbConnection())
    //    {
    //        if (connection != null && connection.State == ConnectionState.Closed)
    //            connection.Open();

    //        //_limite = connection.QueryFirstOrDefault<long>(_rawSqlDPLimite, new { pIdcliente = _idCliente });
    //        //Console.WriteLine($"SQLServer_DP_Select_Limite_01: {_limite}");

    //        //_limite = connection.ExecuteScalar<long>(_rawSqlDPLimite, new { pIdcliente = _idCliente });
    //        //Console.WriteLine($"SQLServer_DP_Select_Limite_01: {_limite}");

    //        cliente = connection.QueryFirstOrDefault<ClienteModel>(_rawSqlDPFull, new { pIdcliente = _idCliente });
    //        Console.WriteLine($"SQLServer_DP_Select_Full_01: {cliente}");

    //        cliente = connection.QuerySingle<ClienteModel>(_rawSqlDPFull, new { pIdcliente = _idCliente });
    //        Console.WriteLine($"SQLServer_DP_Select_Full_03: {cliente}");

    //        cliente = connection.QueryFirst<ClienteModel>(_rawSqlDPFull, new { pIdcliente = _idCliente });
    //        Console.WriteLine($"SQLServer_DP_Select_Full_04: {cliente}");
    //    }
    //}

}
catch (Exception ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(ex.Message);
    Console.ResetColor();
}
finally 
{
    Console.ReadLine();
}

// dotnet run TesteBenchmarkDotNet.csproj -c Release
// dotnet run -p TesteBenchmarkDotNet.csproj -c Release

// dotnet build -c Release
// dotnet C:\Users\chris\Desktop\CMS DotNet\CMS DotNet Teste Outros\TesteBenchmarkDotNet\bin\Release\net7.0\TesteBenchmarkDotNet.dll

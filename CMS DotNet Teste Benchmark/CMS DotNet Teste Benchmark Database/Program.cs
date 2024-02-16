using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Npgsql;
using System.ComponentModel;
using System.Data;
using TesteBenchmarkDotNet.Benchmarks;
using TesteBenchmarkDotNet.Data;
using TesteBenchmarkDotNet.Models;

// https://nishanc.medium.com/writing-better-performant-queries-with-linq-on-ef-core-6-0-%EF%B8%8F-85a1a406879

try
{
    //BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
    //BenchmarkRunner.Run<BenchmarkSelectWhere>(); // BenchmarkSelectWhere // BenchmarkOrderByWhere // BenchmarkMultipleInclude // BenchmarkUnnecessaryJoins

    //using System.Data.SQLite;
    //var connectionString = @"Data Source=C:\Users\chris\AppData\Roaming\DBeaverData\workspace6\.metadata\sample-database-sqlite-1\Chinook.db;Version=3;";
    //using (var conn = new SQLiteConnection(connectionString))

    //using MySql.Data.MySqlClient;
    //var connectionString = "Server=localhost;port=3306;Database=rinha_backend_2024;Uid=root;Pwd=Chrs8723;";
    //using (var conn = new MySqlConnection(connectionString))

    //var connectionString = "Host=127.0.0.1;Port=5432;Database=postgres;User ID=postgres;Password=postgres;Pooling=true;MinPoolSize=1;MaxPoolSize=20;CommandTimeout=20;Timeout=15;";
    //var connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres;Pooling=true;MinPoolSize=1;MaxPoolSize=20;CommandTimeout=60;Timeout=60;Maximum Pool Size=150;Cancellation Timeout=0;No Reset On Close=true"; // localhost // 127.0.0.1
    //using (var conn = new NpgsqlConnection(connectionString))
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

    //    //using (var command = new SQLiteCommand("SELECT * FROM Cliente", conn))
    //    //using (var command = new MySqlCommand("SELECT * FROM Cliente", conn))
    //    using (var command = conn.CreateCommand())
    //    {
    //        command.CommandText = "SELECT * FROM cliente";
    //        using var reader = command.ExecuteReader();

    //        while (reader.Read())
    //        {
    //            //Console.WriteLine($"ID: {reader["Id"]}, Nome: {reader["Nome"]}, Idade: {reader["Idade"]}");
    //            //Console.WriteLine($"ID: {reader["AlbumId"]}, Title: {reader["Title"]}");
    //            Console.WriteLine($"Id: {reader["id"]}, Limite: {reader["limite"]}");
    //        }
    //    }
    //}

    short _idCliente = 1;
    long _limite = 0;
    //string _rawSqlEFLimite = @"SELECT limite FROM Cliente WITH(NOLOCK) WHERE idcliente = {0}";
    //string _rawSqlEFLimite = "SELECT limite as Value FROM Cliente WITH(NOLOCK) WHERE IdCliente = @idCliente";
    //string _rawSqlDPLimite = @"SELECT limite FROM Cliente WITH(NOLOCK) WHERE idcliente = @pIdcliente";
    //ClienteModel cliente = default;
    //string _rawSqlEFFull = @"SELECT * FROM Cliente WITH(NOLOCK) WHERE idcliente = {0}";
    //string _rawSqlDPFull = @"SELECT * FROM Cliente WITH(NOLOCK) WHERE idcliente = @pIdcliente";

    using (var context = new DataContextPostgreSQL()) // DataContextSQLite // DataContextMySQL // DataContextSQLServer // DataContextPostgreSQL
    {

        //context.Database.EnsureDeleted(); //context.Database.Delete();
        //context.Database.EnsureCreated(); //context.Database.Create();

        //foreach (var cliente in context.Clientes)
        //    Console.WriteLine($"ID: {cliente.Id}, Limite: {cliente.Limite}");

        //_limite = context.Clientes.AsNoTracking().Where(x => x.IdCliente == _idCliente).Select(x => x.Limite).FirstOrDefault();
        //Console.WriteLine($"SQLServer_EF_WhereFirst_Limite: {_limite}");

        //_limite = (long)context.Database.SqlQueryRaw<long>(sql: _rawSqlEFLimite, parameters: new SqlParameter("@idCliente", _idCliente)).FirstOrDefault();
        //Console.WriteLine($"SQLServer_EF_Select_Limite: {_limite}");

        //cliente = context.Clientes.FromSqlRaw(_rawSqlEFFull, _idCliente).AsNoTracking().FirstOrDefault();
        //Console.WriteLine($"SQLServer_EF_Select_Full: {cliente}");

        //cliente = context.Clientes.AsNoTracking().Where(x => x.IdCliente == _idCliente).Select(x => new ClienteModel(x.IdCliente, x.Limite)).FirstOrDefault();
        //Console.WriteLine($"SQLServer_EF_WhereFirst_Full: {cliente}");

        //cliente = context.Clientes.AsNoTracking().AsEnumerable().Select(x => new ClienteModel(x.IdCliente, x.Limite)).Where(x => x.IdCliente == _idCliente).ToList().FirstOrDefault();
        //Console.WriteLine($"SQLServer_EF_SelectFirst_Full: {cliente}");

        //using (var connection = new SqlConnection(@"Data Source=127.0.0.1,5402;Initial Catalog=RinhaBackend2024;User ID=sa;Password=Hello123#;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
        //using (var connection = context.Database.GetDbConnection() as SqlConnection)
        //using (var connection = context.GetSqlConnection())
        //using (var connection = (SqlConnection)context.Database.GetDbConnection())
        using (var connection = (NpgsqlConnection)context.Database.GetDbConnection())
        {
            if (connection != null && connection.State == ConnectionState.Closed) connection.Open();

            //_limite = connection.QueryFirstOrDefault<long>(_rawSqlDPLimite, new { pIdcliente = _idCliente });
            //Console.WriteLine($"SQLServer_DP_Select_Limite_01: {_limite}");

            //_limite = connection.ExecuteScalar<long>(_rawSqlDPLimite, new { pIdcliente = _idCliente });
            //Console.WriteLine($"SQLServer_DP_Select_Limite_01: {_limite}");

            //cliente = connection.QueryFirstOrDefault<ClienteModel>(_rawSqlDPFull, new { pIdcliente = _idCliente });
            //Console.WriteLine($"SQLServer_DP_Select_Full_01: {cliente}");

            //cliente = connection.QuerySingle<ClienteModel>(_rawSqlDPFull, new { pIdcliente = _idCliente });
            //Console.WriteLine($"SQLServer_DP_Select_Full_03: {cliente}");

            //cliente = connection.QueryFirst<ClienteModel>(_rawSqlDPFull, new { pIdcliente = _idCliente });
            //Console.WriteLine($"SQLServer_DP_Select_Full_04: {cliente}");

            using(var command1 = connection.CreateCommand())// command // cmd
            {
                _limite = 0;
                command1.CommandText = "SELECT limite FROM cliente WHERE id = @pIdcliente";
                command1.Parameters.AddWithValue("pIdcliente", _idCliente);
                _limite = Convert.ToInt64(await command1.ExecuteScalarAsync());
                Console.WriteLine($"Limite-01: {_limite}");
            }

            using (var command2 = connection.CreateCommand())// command // cmd
            {
                _limite = 0;
                command2.CommandText = "SELECT limite FROM cliente WHERE id = @pIdcliente";
                command2.Parameters.AddWithValue("pIdcliente", _idCliente);
                await using var reader1 = await command2.ExecuteReaderAsync();
                if (await reader1.ReadAsync()) _limite = reader1.GetInt64(0);
                Console.WriteLine($"Limite-02: {_limite}");
            }

            using (var command3 = connection.CreateCommand())// command // cmd
            {
                command3.CommandText = "SELECT id, limite FROM cliente WHERE id = @pIdcliente";
                command3.Parameters.AddWithValue("pIdcliente", _idCliente);
                await using var reader2 = await command3.ExecuteReaderAsync();
                if (await reader2.ReadAsync().ConfigureAwait(false))
                {
                    var cliente = new ClienteModel(reader2.GetInt16(0), reader2.GetInt64(1));
                    Console.WriteLine($"Cliente-01: {cliente}");
                }
            }

            using (var command4 = connection.CreateCommand())// command // cmd
            {
                command4.CommandText = "SELECT id, limite FROM cliente";
                await using var reader3 = await command4.ExecuteReaderAsync().ConfigureAwait(false);
                // var result = new List<ClienteModel>();
                while (await reader3.ReadAsync().ConfigureAwait(false))
                {
                    var cliente = new ClienteModel(reader3.GetInt16(0), reader3.GetInt64(1));
                    Console.WriteLine($"Cliente-02: {cliente}");
                    // result.Add(new ClienteModel());
                }
            }
        }
    }

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

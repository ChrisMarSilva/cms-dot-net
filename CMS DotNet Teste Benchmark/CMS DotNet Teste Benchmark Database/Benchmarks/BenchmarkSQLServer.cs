using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Data;
using TesteBenchmarkDotNet.Data;

namespace TesteBenchmarkDotNet.Benchmarks;

public class BenchmarkSQLServer : IDisposable
{
    //protected readonly DataContextSQLServer ContextSQLServer;
    //protected readonly SqlConnection ConnectionSQLServer;

    protected readonly DataContextPostgreSQL ContextPostgreSQL;
    protected readonly NpgsqlConnection ConnectionPostgreSQL;

    public BenchmarkSQLServer()
    {
        //ContextSQLServer = new DataContextSQLServer();
        ////ConnectionSQLServer = new SqlConnection(@"Data Source=127.0.0.1,5402;Initial Catalog=RinhaBackend2024;User ID=sa;Password=Hello123#;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        ////ConnectionSQLServer = ContextSQLServer.Database.GetDbConnection() as SqlConnection;
        //ConnectionSQLServer = (SqlConnection)ContextSQLServer.Database.GetDbConnection();
        //if (ConnectionSQLServer != null && ConnectionSQLServer.State == ConnectionState.Closed)
        //    ConnectionSQLServer.Open();

        ContextPostgreSQL = new DataContextPostgreSQL();
        ConnectionPostgreSQL = (NpgsqlConnection)ContextPostgreSQL.Database.GetDbConnection();
        if (ConnectionPostgreSQL != null && ConnectionPostgreSQL.State == ConnectionState.Closed)
            ConnectionPostgreSQL.Open();
    }

    // [GlobalSetup] public async Task Init() { }

    public void Dispose()
    {
        //ContextSQLServer.Dispose();
        ContextPostgreSQL.Dispose();
    }
}

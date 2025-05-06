using Dapper;
using Npgsql;
using System.Text.Json;
using TesteBenchmarkDotNet.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TesteBenchmarkDotNet.Service;

public class PostgresService
{
    private readonly string _conn;

    public PostgresService(string conn) => _conn = conn;

    public async Task InsertAsync(TestData data)
    {
        using var connection = new NpgsqlConnection(_conn);

        var sql = """
            INSERT INTO test_data (name, value)
            VALUES (@Name, @Value::jsonb)
            ON CONFLICT (name) DO UPDATE
            SET value = excluded.value;
        """;

        //EXCLUDED.value::jsonb

        //await connection.ExecuteAsync(sql, new { data.Name, Value = data.Value.ToString() });
        await connection.ExecuteAsync(sql, new { data.Name, Value = JsonSerializer.Serialize(data.Value) });
    }

    public async Task<TestData?> GetAsync(string name)
    {
        using var connection = new NpgsqlConnection(_conn);

        var sql = """
            SELECT id, name, value 
            FROM test_data 
            WHERE name = @name
        """;

        return await connection.QuerySingleOrDefaultAsync<TestData>(sql, new { name });
    }
}
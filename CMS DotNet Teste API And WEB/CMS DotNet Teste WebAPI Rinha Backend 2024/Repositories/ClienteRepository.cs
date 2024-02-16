using Npgsql;
using Rinha.Backend._2024.API.Dtos;
using Rinha.Backend._2024.API.Repositories.Interfaces;
using System.Data;

namespace Rinha.Backend._2024.API.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly NpgsqlDataSource _conn; // IDbConnection // NpgsqlDataSource // NpgsqlConnection

    public ClienteRepository(NpgsqlDataSource conn)
    {
        _conn = conn;
    }

    public async Task<ClienteDto?> GetByIdAsync(int id)
    {
        //var sql = "SELECT Limite, Saldo FROM Cliente WITH(NOLOCK) WHERE IdCliente = @pIdcliente"; // SQL Server
        var sql = "SELECT limite, saldo FROM cliente WHERE id = @pIdcliente"; // Postgres

        //var param = new DynamicParameters();
        //param.Add("@pIdcliente", id);

        //var results = await _conn.QueryAsync<ClienteDto>(sql, param);
        //var results = (await _conn.QueryAsync<ClienteDto>(sql, param)).ToList();

        await using var command = _conn.CreateCommand(sql);
        //await using var command = new NpgsqlCommand(sql, _conn);

        command.Parameters.Clear();
        command.Parameters.AddWithValue("pIdcliente", id);

        await using var reader = await command.ExecuteReaderAsync().ConfigureAwait(false);

        ClienteDto result = null;
        if (await reader.ReadAsync().ConfigureAwait(false))
            result = new ClienteDto { Limite = reader.GetInt64(0), Saldo = reader.GetInt64(1) };

        return result;
    }

    public async Task<long> GetLimiteAsync(int id)
    {
        //var sql = "SELECT Limite FROM Cliente WITH(NOLOCK) WHERE IdCliente = @pIdcliente"; // SQL Server
        var sql = "SELECT limite FROM cliente WHERE id = @pIdcliente"; // Postgres

        //var param = new DynamicParameters();
        //param.Add("@pIdcliente", id);
        //result = await _conn.QueryFirstOrDefaultAsync<long>(sql, param);

        await using var command = _conn.CreateCommand(sql);
        //await using var command = new NpgsqlCommand(sql, _conn);

        command.Parameters.Clear();
        command.Parameters.AddWithValue("pIdcliente", id);

        //using var command2 = new NpgsqlCommand("SELECT * FROM some_function($1, $2)", _conn)
        //{
        //    new() { Value = "some_value" },
        //    new() { Value = "some_other_value" }
        //};

        //var parameter = new NpgsqlParameter();
        //parameter.ParameterName = "@pIdcliente";
        //parameter.Value = id;
        //var parameter = new NpgsqlParameter("pIdcliente", id);
        //command.Parameters.Add(parameter);

        await using var reader = await command.ExecuteReaderAsync().ConfigureAwait(false);

        long result = default;
        if (await reader.ReadAsync().ConfigureAwait(false)) 
            result = reader.GetInt64(0);

        //var result = Convert.ToInt64(command.ExecuteScalar());

        return result;
    }

    public async Task<long> GetSaldoAsync(int id)
    {
        // var sql = "SELECT Saldo FROM Cliente WITH(NOLOCK) WHERE IdCliente = @pIdcliente"; // SQL Server
        var sql = "SELECT saldo FROM cliente WHERE id = @pIdcliente"; // Postgres

        //var param = new DynamicParameters();
        //param.Add("@pIdcliente", id);

        // var result = await _conn.QueryFirstOrDefaultAsync<long>(sql, param);

        await using var command = _conn.CreateCommand(sql);
        //await using var command = new NpgsqlCommand(sql, _conn);

        command.Parameters.Clear();
        command.Parameters.AddWithValue("pIdcliente", id);

        await using var reader = await command.ExecuteReaderAsync().ConfigureAwait(false);

        long result = default;
        if (await reader.ReadAsync().ConfigureAwait(false))
            result = reader.GetInt64(0);

        return result;
    }

    public async Task<bool> UpdateSaldoAsync(int id, string tipo, long valor, IDbTransaction? transaction = null)
    {
        //var sql = "UPDATE Cliente SET Saldo = Saldo " + (tipo.Equals("d") ? "-" : "+") + " @pValor WHERE IdCliente = @pIdcliente"; // SQL Server
        //var sql = "UPDATE cliente SET saldo = saldo " + (tipo.ToLower().Equals("d") ? "-" : "+") + " @pValor WHERE id = @pIdcliente";// Postgres

        var sql = tipo.ToLower() switch
        {
            "d" => "UPDATE cliente SET saldo = saldo - @pValor WHERE id = @pIdcliente",
            "c" => "UPDATE cliente SET saldo = saldo + @pValor WHERE id = @pIdcliente",
            _ => ""
        };

        //var param = new DynamicParameters();
        //param.Add("@pValor", valor);
        //param.Add("@pIdcliente", id);

        //var affectedRows = await _conn.ExecuteAsync(sql, param, transaction);

        //await using var command = new NpgsqlCommand(sql, (NpgsqlConnection)_conn, (NpgsqlTransaction)transaction!);
        //await using var command = new NpgsqlCommand(sql, _conn, (NpgsqlTransaction)transaction);
        await using var command = _conn.CreateCommand(sql);

        command.Parameters.Clear();
        command.Parameters.AddWithValue("pValor", NpgsqlTypes.NpgsqlDbType.Integer, valor);
        command.Parameters.AddWithValue("pIdcliente", NpgsqlTypes.NpgsqlDbType.Integer, id);

        var affectedRows = await command.ExecuteNonQueryAsync().ConfigureAwait(false);

        return affectedRows == 1;
    }
}

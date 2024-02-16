using Npgsql;
using Rinha.Backend._2024.API.Repositories.Interfaces;
using System.Data;

namespace Rinha.Backend._2024.API.Repositories;

public class ClienteCarteiraRepository : IClienteCarteiraRepository
{
    private readonly NpgsqlDataSource _conn; // IDbConnection // NpgsqlDataSource // NpgsqlConnection

    public ClienteCarteiraRepository(NpgsqlDataSource conn)
    {
        _conn = conn;
    }

    public async Task<long> GetSaldoAsync(int idCliente)
    {
        // var sql = "SELECT Saldo FROM ClienteCarteira WITH(NOLOCK) WHERE IdCliente = @pIdcliente"; // SQL Server
        var sql = "SELECT total FROM cliente_saldo WHERE cliente_id = @pIdcliente"; // Postgres

        //var param = new DynamicParameters();
        //param.Add("@pIdcliente", idCliente);

        // var result = await _conn.QueryFirstOrDefaultAsync<long>(sql, param);

        await using var command = _conn.CreateCommand(sql);
        //await using var command = new NpgsqlCommand(sql, _conn);

        command.Parameters.Clear();
        command.Parameters.AddWithValue("pIdcliente", idCliente);

        await using var reader = await command.ExecuteReaderAsync().ConfigureAwait(false);

        long result = default;
        if (await reader.ReadAsync().ConfigureAwait(false))
            result = reader.GetInt64(0);

        return result;
    }

    public async Task<bool> UpdateSaldoAsync(int idCliente, string tipo, long valor, IDbTransaction? transaction = null)
    {
        //var sql = "UPDATE ClienteCarteira SET Saldo = Saldo " + (tipo.Equals("d") ? "-" : "+") + " @pValor WHERE idcliente = @pIdcliente"; // SQL Server
        //var sql = "UPDATE cliente_saldo SET total = total " + (tipo.ToLower().Equals("d") ? "-" : "+") + " @pValor WHERE cliente_id = @pIdcliente";// Postgres

        var sql = tipo.ToLower() switch
        {
            "d" => "UPDATE cliente_saldo SET total = total - @pValor WHERE cliente_id = @pIdcliente",
            "c" => "UPDATE cliente_saldo SET total = total + @pValor WHERE cliente_id = @pIdcliente",
            _ => ""
        };

        //var param = new DynamicParameters();
        //param.Add("@pValor", valor);
        //param.Add("@pIdcliente", idCliente);

        //var affectedRows = await _conn.ExecuteAsync(sql, param, transaction);

        //await using var command = new NpgsqlCommand(sql, (NpgsqlConnection)_conn, (NpgsqlTransaction)transaction!);
        //await using var command = new NpgsqlCommand(sql, _conn, (NpgsqlTransaction)transaction);
        await using var command = _conn.CreateCommand(sql);

        command.Parameters.Clear();
        command.Parameters.AddWithValue("pValor", NpgsqlTypes.NpgsqlDbType.Integer, valor);
        command.Parameters.AddWithValue("pIdcliente", NpgsqlTypes.NpgsqlDbType.Integer, idCliente);

        var affectedRows = await command.ExecuteNonQueryAsync().ConfigureAwait(false);

        return affectedRows == 1;
    }
}

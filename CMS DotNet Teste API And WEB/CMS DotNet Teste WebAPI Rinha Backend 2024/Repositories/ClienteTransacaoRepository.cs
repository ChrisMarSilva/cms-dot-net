using Npgsql;
using Rinha.Backend._2024.API.Dtos;
using Rinha.Backend._2024.API.Repositories.Interfaces;
using System.Data;

namespace Rinha.Backend._2024.API.Repositories;

public class ClienteTransacaoRepository : IClienteTransacaoRepository
{
    private readonly NpgsqlDataSource _conn; // IDbConnection // NpgsqlDataSource // NpgsqlConnection

    public ClienteTransacaoRepository(NpgsqlDataSource conn)
    {
        _conn = conn;
    }

    public async Task<IEnumerable<ClienteTransacaoDto>> GetAllAsync(int idCliente)
    {

        // var sql = "SELECT TOP(10) Valor, Tipo, Descricao, DtHrRegistro FROM ClienteTransacao WITH(NOLOCK) WHERE IdCliente = @pIdcliente ORDER BY IdTransacao DESC"; // IdTransacao // dthrregistro // SQL Server
        var sql = "SELECT valor, tipo, descricao, dthrregistro FROM cliente_transacao WHERE cliente_id = @pIdcliente ORDER BY id DESC LIMIT 10"; // fetch first 10 rows only // Postgres

        //var param = new DynamicParameters();
        //param.Add("@pIdcliente", idCliente);

        //var results = await _conn.QueryAsync<ClienteTransacaoReadModel>(sql, param);
        //var results = (await _conn.QueryAsync<ClienteTransacaoReadModel>(sql, param)).ToList();

        await using var command = _conn.CreateCommand(sql);
        //await using var command = new NpgsqlCommand(sql, _conn);

        command.Parameters.Clear();
        command.Parameters.AddWithValue("pIdcliente", idCliente);

        await using var reader = await command.ExecuteReaderAsync().ConfigureAwait(false);

        var results = new List<ClienteTransacaoDto>();

        while (await reader.ReadAsync().ConfigureAwait(false))
        {
            var clienteTransacao = new ClienteTransacaoDto
            {
                IdCliente = idCliente,
                Valor = reader.GetInt64(0),
                Tipo = reader.GetString(1),
                Descricao = reader.GetString(2),
                DtHrRegistro = reader.GetDateTime(3)
            };

            results.Add(clienteTransacao);
        }

        return results;
    }

    public async Task<bool> AddAsync(int idCliente, long valor, string tipo, string descricao, IDbTransaction? transaction = null)
    {
        // var sql = "INSERT INTO ClienteTransacao (IdCliente, Valor, Tipo, Descricao, DtHrRegistro) Values (@pIdcliente, @pValor, @pTipo, @pDescricao, @pDtHrRegistro)"; // SQL Server
        var sql = "INSERT INTO cliente_transacao (cliente_id, valor, tipo, descricao, dthrregistro) Values (@pIdcliente, @pValor, @pTipo, @pDescricao, @pDtHrRegistro)";// Postgres

        //var param = new DynamicParameters();
        //param.Add("@pIdcliente", idCliente);
        //param.Add("@pValor", valor);
        //param.Add("@pTipo", tipo.ToLower());
        //param.Add("@pDescricao", descricao);
        //param.Add("@pDtHrRegistro", DateTime.Now);

        //var affectedRows = await _conn.ExecuteAsync(sql, param, transaction);

        //await using var command = new NpgsqlCommand(sql, (NpgsqlConnection)_conn, (NpgsqlTransaction)transaction!);
        //await using var command = new NpgsqlCommand(sql, _conn, (NpgsqlTransaction)transaction);
        await using var command = _conn.CreateCommand(sql);

        command.Parameters.Clear();
        command.Parameters.AddWithValue("pIdcliente", NpgsqlTypes.NpgsqlDbType.Integer, idCliente);
        command.Parameters.AddWithValue("pValor", NpgsqlTypes.NpgsqlDbType.Integer, valor);
        command.Parameters.AddWithValue("pTipo", NpgsqlTypes.NpgsqlDbType.Varchar, tipo.ToLower());
        command.Parameters.AddWithValue("pDescricao", NpgsqlTypes.NpgsqlDbType.Varchar, descricao);
        command.Parameters.AddWithValue("pDtHrRegistro", NpgsqlTypes.NpgsqlDbType.Timestamp, DateTime.Now);

        var affectedRows = await command.ExecuteNonQueryAsync().ConfigureAwait(false);

        return affectedRows == 1;
    }
}

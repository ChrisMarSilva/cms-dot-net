using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Rinha.Backend._2024.API.Context;
using Rinha.Backend._2024.API.Models.Dtos;
using Rinha.Backend._2024.API.Models.Read;
using Rinha.Backend._2024.API.Models.Write;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Rinha.Backend._2024.API.Endpoints;

public static class ClientesEndpoints
{
    public static void UseMapClientesEndpoints(this WebApplication app)
    {
        app.MapPost("/clientes/{id:int}/transacoes", async (short id, [FromBody] TransacaoRequestDto request, [FromServices] AppReadDbContext contextRead, [FromServices] AppWriteDbContext contextWrite) =>
        {
            try
            {
                if (!request.IsValid()) return Results.UnprocessableEntity("Payload inválido.");

                var connection = (SqlConnection)contextRead.Database.GetDbConnection();
                if (connection is null) return Results.UnprocessableEntity("Conexão inválida.");
                if  (connection.State == ConnectionState.Closed) connection.Open();

                var limiteCliente = await connection.QueryFirstOrDefaultAsync<long>("SELECT limite FROM Cliente WITH(NOLOCK) WHERE idcliente = @pIdcliente", new { pIdcliente = id });
                if (limiteCliente == default) return Results.NotFound("Cliente não localizado.");

                var saldoCarteira = await connection.QueryFirstOrDefaultAsync<long>("SELECT saldo FROM ClienteCarteira WITH(NOLOCK) WHERE idcliente = @pIdcliente", new { pIdcliente = id });

                long novoSado = default;
                if (request.Tipo!.Equals("d", StringComparison.OrdinalIgnoreCase)) novoSado = saldoCarteira + limiteCliente - request.Valor!.Value;
                else novoSado = saldoCarteira + limiteCliente + request.Valor!.Value;
                if (novoSado < 0) return Results.UnprocessableEntity("Novo saldo do cliente menor que seu limite disponível.");
                novoSado -= limiteCliente;

                using (var transaction = await connection.BeginTransactionAsync())
                {
                    try
                    {
                        string sqlUpdCarteira = "UPDATE ClienteCarteira SET Saldo = @pSaldo WHERE idcliente = @pIdcliente";
                        object[] paramUpdCarteira = { new { pSaldo = novoSado, pIdcliente = id } };
                        var affectedRowsUpdCarteira = await connection.ExecuteAsync(sqlUpdCarteira, paramUpdCarteira, transaction);

                        string sqlInsTransacao = "INSERT INTO ClienteTransacao (IdCliente, Valor, Tipo, Descricao, DtHrRegistro) Values (@pIdcliente, @pValor, @pTipo, @pDescricao, @pDtHrRegistro)";
                        object[] paramInsTransacao = { new { pIdcliente = id, pValor = request.Valor!.Value, pTipo = request.Tipo.ToLower(), pDescricao = request.Descricao!, pDtHrRegistro = DateTime.Now } };
                        var affectedRowsInsTransacao = await connection.ExecuteAsync(sqlInsTransacao, paramInsTransacao, transaction);

                        await transaction.CommitAsync();
                    }
                    catch
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }

                var response = new TransacaoResponseDto(Limite: limiteCliente, Saldo: novoSado);
                return Results.Ok(response); 
            }
            catch (Exception ex)
            {
                return Results.UnprocessableEntity(ex.Message); 
            }
        }).Accepts<TransacaoRequestDto>("application/json")
          .Produces<TransacaoResponseDto>(StatusCodes.Status200OK)
          .Produces(StatusCodes.Status404NotFound)
          .Produces(StatusCodes.Status422UnprocessableEntity)
          .WithName("Transacoes")
          .WithTags("Clientes");

        app.MapGet("/clientes/{id:int}/extrato", async (short id, [FromServices] AppReadDbContext contextRead) =>
        {
            try
            {
                var cliente = await contextRead.Clientes
                    .AsNoTracking()
                    .Where(x => x.IdCliente == id)
                    .Select(x => new ClienteReadModel { IdCliente = x.IdCliente, Limite = x.Limite })
                    .FirstOrDefaultAsync();

                if (cliente is null) return Results.NotFound("Cliente não localizado.");

                var carteira = await contextRead.Carteiras
                    .AsNoTracking()
                    .Where(x => x.IdCliente == id)
                    .Select(x => new ExtratoSaldoResponseDto(x.Saldo, DateTime.Now, cliente.Limite))
                    .FirstOrDefaultAsync();

                var transacoes = await contextRead.Transacoes
                  .AsNoTracking()
                  .Where(x => x.IdCliente == id)
                  .OrderByDescending(c => c.DtHrRegistro)
                  .Take(10)
                  .Select(x => new ExtratoTransacoesResponseDto(x.Valor, x.Tipo, x.Descricao, x.DtHrRegistro.ToString("yyyy-MM-ddTHH:mm:ss.ffffffZ", System.Globalization.CultureInfo.InvariantCulture)))
                  .ToListAsync();

                var response = new ExtratoResponseDto(saldo: carteira!, Transacoes: transacoes);

                return Results.Ok(response);
            }
            catch (Exception ex)
            {
                return Results.UnprocessableEntity(ex.Message);
            }
        }).Produces<ExtratoResponseDto>(StatusCodes.Status200OK)
          .Produces(StatusCodes.Status404NotFound)
          .Produces(StatusCodes.Status422UnprocessableEntity)
          .WithName("Extrato")
          .WithTags("Clientes");
    }
}

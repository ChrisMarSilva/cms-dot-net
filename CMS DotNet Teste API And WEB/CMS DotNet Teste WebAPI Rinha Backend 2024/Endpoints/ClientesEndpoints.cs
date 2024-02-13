using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Rinha.Backend._2024.API.Context;
using Rinha.Backend._2024.API.Models.Domains.Read;
using Rinha.Backend._2024.API.Models.Dtos.RequestDtos;
using Rinha.Backend._2024.API.Models.Dtos.ResponseDtos;
using System.Data;

namespace Rinha.Backend._2024.API.Endpoints;

public static class ClientesEndpoints
{
    public static void UseMapClientesEndpoints(this WebApplication app)
    {
        app.MapPost("/clientes/{id:int}/transacoes", 
            async (short id, [FromBody] TransacaoRequestDto request, [FromServices] AppReadDbContext contextRead, [FromServices] AppWriteDbContext contextWrite, CancellationToken cancellationToken) =>
        {
            try
            {
                if (!request.Valido()) Results.UnprocessableEntity("Payload inválido.");

                var connection = (SqlConnection)contextRead.Database.GetDbConnection();
                if (connection is null) return Results.UnprocessableEntity("Conexão inválida.");
                if  (connection.State == ConnectionState.Closed) connection.Open();

                var limiteCliente = await connection.QueryFirstOrDefaultAsync<long>("SELECT limite FROM Cliente WITH(NOLOCK) WHERE idcliente = @pIdcliente", new { pIdcliente = id }, commandTimeout: 60);
                if (limiteCliente == default) return Results.NotFound("Cliente não localizado.");

                var saldoCarteira = await connection.QueryFirstOrDefaultAsync<long>("SELECT saldo FROM ClienteCarteira WITH(NOLOCK) WHERE idcliente = @pIdcliente", new { pIdcliente = id }, commandTimeout: 60);

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
                        var affectedRowsUpdCarteira = await connection.ExecuteAsync(sqlUpdCarteira, paramUpdCarteira, transaction, commandTimeout: 60);

                        string sqlInsTransacao = "INSERT INTO ClienteTransacao (IdCliente, Valor, Tipo, Descricao, DtHrRegistro) Values (@pIdcliente, @pValor, @pTipo, @pDescricao, @pDtHrRegistro)";
                        object[] paramInsTransacao = { new { pIdcliente = id, pValor = request.Valor!.Value, pTipo = request.Tipo.ToLower(), pDescricao = request.Descricao!, pDtHrRegistro = DateTime.Now } };
                        var affectedRowsInsTransacao = await connection.ExecuteAsync(sqlInsTransacao, paramInsTransacao, transaction, commandTimeout: 60);

                        await transaction.CommitAsync();
                    }
                    catch
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }

                var response = new TransacaoResponseDto { Limite = limiteCliente,  Saldo = novoSado };

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

        app.MapGet("/clientes/{id:int}/extrato", 
            async (short id, [FromServices] AppReadDbContext contextRead, CancellationToken cancellationToken) =>
        {
            try
            {
                var connection = (SqlConnection)contextRead.Database.GetDbConnection();
                if (connection is null) return Results.UnprocessableEntity("Conexão inválida.");
                if (connection.State == ConnectionState.Closed) connection.Open();

                var limiteCliente = await connection.QueryFirstOrDefaultAsync<long>("SELECT limite FROM Cliente WITH(NOLOCK) WHERE idcliente = @pIdcliente", new { pIdcliente = id }, commandTimeout: 60);
                if (limiteCliente == default) return Results.NotFound("Cliente não localizado.");

                var saldoCarteira = await connection.QueryFirstOrDefaultAsync<long>("SELECT saldo FROM ClienteCarteira WITH(NOLOCK) WHERE idcliente = @pIdcliente", new { pIdcliente = id }, commandTimeout: 60);

                var transacoes = await connection.QueryAsync<ClienteTransacaoReadModel>("SELECT TOP(10) Valor, Tipo, Descricao, DtHrRegistro FROM ClienteTransacao WITH(NOLOCK) WHERE idcliente = @pIdcliente ORDER BY dthrregistro DESC", new { pIdcliente = id }, commandTimeout: 60);

                var response = new ExtratoResponseDto
                {
                    Saldo = new ExtratoSaldoResponseDto { Total = saldoCarteira, Data_Extrato = DateTime.Now, Limite = limiteCliente },
                    Transacoes = transacoes?.Select(x => new ExtratoTransacoesResponseDto { Valor = x.Valor, Tipo = x.Tipo, Descricao = x.Descricao, Realizada_Em = x.DtHrRegistro }).ToList()
                };

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

using Azure;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rinha.Backend._2024.API.Models.Domains.Read;
using Rinha.Backend._2024.API.Models.Dtos.RequestDtos;
using Rinha.Backend._2024.API.Models.Dtos.ResponseDtos;
using System.Data;

namespace Rinha.Backend._2024.API.Endpoints;

public static class ClientesEndpoints
{
    public static void UseMapClientesEndpoints(this WebApplication app)
    {
        app.MapPost("/clientes/{id:int}/transacoes", async (short id, [FromBody] TransacaoRequestDto request, [FromServices] IDbConnection connection, CancellationToken cancellationToken) =>
        {
            try
            {
                if (!request.Valido()) Results.UnprocessableEntity("Payload inválido.");

                if (connection is null) return Results.UnprocessableEntity("Conexão inválida.");
                if  (connection.State == ConnectionState.Closed) connection.Open();

                var limiteCliente = await connection.QueryFirstOrDefaultAsync<long>("SELECT Limite FROM Cliente WITH(NOLOCK) WHERE IdCliente = @pIdcliente", new { pIdcliente = id }, commandTimeout: 60);
                if (limiteCliente == default) return Results.NotFound("Cliente não localizado.");

                var saldoCarteira = await connection.QueryFirstOrDefaultAsync<long>("SELECT Saldo FROM ClienteCarteira WITH(NOLOCK) WHERE IdCliente = @pIdcliente", new { pIdcliente = id }, commandTimeout: 60);

                long valor = request.Valor!.Value;
                string tipo = request.Tipo!.ToLower();
                string descricao = request.Descricao!;

                long novoSado = default;
                if (tipo.Equals("d")) novoSado = saldoCarteira + limiteCliente - valor;
                else novoSado = saldoCarteira + limiteCliente + valor;
                
                if (novoSado < 0) return Results.UnprocessableEntity("Novo saldo do cliente menor que seu limite disponível.");

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var sqlUpdCarteira = "UPDATE ClienteCarteira SET Saldo = Saldo " + (tipo.Equals("d") ? "-" : "+") + " @pValor WHERE idcliente = @pIdcliente";
                        object[] paramUpdCarteira = { new { pValor = valor, pIdcliente = id } };
                        var affectedRowsUpdCarteira = await connection.ExecuteAsync(sqlUpdCarteira, paramUpdCarteira, transaction, commandTimeout: 60);

                        var sqlInsTransacao = "INSERT INTO ClienteTransacao (IdCliente, Valor, Tipo, Descricao, DtHrRegistro) Values (@pIdcliente, @pValor, @pTipo, @pDescricao, @pDtHrRegistro)";
                        object[] paramInsTransacao = { new { pIdcliente = id, pValor = valor, pTipo = tipo, pDescricao = descricao, pDtHrRegistro = DateTime.Now } };
                        var affectedRowsInsTransacao = await connection.ExecuteAsync(sqlInsTransacao, paramInsTransacao, transaction, commandTimeout: 60);

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
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

        app.MapGet("/clientes/{id:int}/extrato", async (short id, [FromServices] IDbConnection connection, CancellationToken cancellationToken) =>
        {
            try
            {
                if (connection is null) return Results.UnprocessableEntity("Conexão inválida.");
                if (connection.State == ConnectionState.Closed) connection.Open();

                var limiteCliente = await connection.QueryFirstOrDefaultAsync<long>("SELECT Limite FROM Cliente WITH(NOLOCK) WHERE IdCliente = @pIdcliente", new { pIdcliente = id }, commandTimeout: 60);
                if (limiteCliente == default) return Results.NotFound("Cliente não localizado.");

                var saldoCarteira = await connection.QueryFirstOrDefaultAsync<long>("SELECT Saldo FROM ClienteCarteira WITH(NOLOCK) WHERE IdCliente = @pIdcliente", new { pIdcliente = id }, commandTimeout: 60);

                //var transacoes = await connection.QueryAsync<ClienteTransacaoReadModel>("SELECT TOP(10) Valor, Tipo, Descricao, DtHrRegistro FROM ClienteTransacao WITH(NOLOCK) WHERE IdCliente = @pIdcliente ORDER BY dthrregistro DESC", new { pIdcliente = id }, commandTimeout: 60);
                var transacoes = await connection.QueryAsync<ClienteTransacaoReadModel>("SELECT TOP(10) Valor, Tipo, Descricao, DtHrRegistro FROM ClienteTransacao WITH(NOLOCK) WHERE IdCliente = @pIdcliente ORDER BY IdTransacao DESC", new { pIdcliente = id }, commandTimeout: 60);

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

        app.MapGet("/ping", () => Results.Ok("Pong"))
         .Produces<string>(StatusCodes.Status200OK)
         .WithName("Ping")
         .WithTags("Clientes");
    }
}

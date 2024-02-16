using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Rinha.Backend._2024.API.Dtos;
using Rinha.Backend._2024.API.Repositories.Interfaces;
using System.Data;

namespace Rinha.Backend._2024.API.Endpoints;

public static class ClientesEndpoints
{
    public static void UseMapClientesEndpoints(this WebApplication app)
    {
        app.MapPost("/clientes/{id:int}/transacoes", async (
            short id, 
            [FromBody] TransacaoRequestDto request,
            [FromServices] NpgsqlConnection connection, // NpgsqlDataSource // NpgsqlConnection // [FromKeyedServices("write")]
            [FromServices] IClienteRepository clienteRepo,
            //[FromServices] IClienteCarteiraRepository carteiraRepo,
            [FromServices] IClienteTransacaoRepository transacaoRepo,
            CancellationToken cancellationToken) =>
        {
            try
            {
                if (!request.Valido()) Results.UnprocessableEntity("Payload inválido.");

                //if (connection is null) return Results.UnprocessableEntity("Conexão inválida.");
                if (connection.State == ConnectionState.Closed) await connection.OpenAsync();
                // await using var conn = await connection.OpenConnectionAsync();
                //await connection.OpenConnectionAsync(cancellationToken);

                //var limiteCliente = await clienteRepo.GetLimiteAsync(id);
                //if (limiteCliente == default) return Results.NotFound("Cliente não localizado.");

                //var saldoCarteira = await carteiraRepo.GetSaldoAsync(id);
                //var saldoCarteira = await clienteRepo.GetSaldoAsync(id);

                var cliente = await clienteRepo.GetByIdAsync(id);
                if (cliente is null) return Results.NotFound("Cliente não localizado.");
                var limiteCliente = cliente.Limite;
                var saldoCarteira = cliente.Saldo;

                long novoSado = default;
                if (request.Tipo!.Equals("d")) novoSado = saldoCarteira + limiteCliente - request.Valor!.Value;
                else novoSado = saldoCarteira + limiteCliente + request.Valor!.Value;
                if (novoSado < 0) return Results.UnprocessableEntity("Novo saldo do cliente menor que seu limite disponível.");
                novoSado -= limiteCliente;

                // var con = await connection.OpenConnectionAsync(cancellationToken);

                //using (var transaction = await connection.BeginTransactionAsync())
                //{
                //    try
                //    {
                //await carteiraRepo.UpdateSaldoAsync(id, request.Tipo, request.Valor.Value, null); // transaction
                await clienteRepo.UpdateSaldoAsync(id, request.Tipo, request.Valor.Value, null);
                await transacaoRepo.AddAsync(id, request.Valor.Value, request.Tipo, request.Descricao!, null);
                //        await transaction.CommitAsync();
                //    }
                //    catch
                //    {
                //        await transaction.RollbackAsync();
                //        throw;
                //    }
                //}

                var response = new TransacaoResponseDto { Limite = limiteCliente, Saldo = novoSado };

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

        app.MapGet("/clientes/{id:int}/extrato", async (
            short id,
            [FromServices] NpgsqlConnection connection, // NpgsqlDataSource // NpgsqlConnection // [FromKeyedServices("read")]
            [FromServices] IClienteRepository clienteRepo,
            //[FromServices] IClienteCarteiraRepository carteiraRepo,
            [FromServices] IClienteTransacaoRepository transacaoRepo,
            CancellationToken cancellationToken) =>
        {
            try
            {
                //if (connection is null) return Results.UnprocessableEntity("Conexão inválida.");
                if (connection.State == ConnectionState.Closed) await connection.OpenAsync();
                //await using var conn = await connection.OpenConnectionAsync();
                //await connection.OpenConnectionAsync(cancellationToken);

                //var limiteCliente = await clienteRepo.GetLimiteAsync(id);
                //if (limiteCliente == default) return Results.NotFound("Cliente não localizado.");

                //var saldoCarteira = await carteiraRepo.GetSaldoAsync(id);
                //var saldoCarteira = await clienteRepo.GetSaldoAsync(id);

                var cliente = await clienteRepo.GetByIdAsync(id);
                if (cliente is null) return Results.NotFound("Cliente não localizado.");
                var limiteCliente = cliente.Limite;
                var saldoCarteira = cliente.Saldo;

                var transacoes = await transacaoRepo.GetAllAsync(id);
              
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

        app.MapGet("/ping", ([FromServices] NpgsqlDataSource connection) =>
        {
            //return Results.Ok("Pong");
            return connection.ConnectionString;
        }).Produces<string>(StatusCodes.Status200OK)
          .WithName("Ping")
          .WithTags("Clientes");

        app.MapGet("/teste", ([FromServices] IConfiguration configuration) =>
        {
            return configuration["MENSAGEM"];
        }).Produces<string>(StatusCodes.Status200OK)
          .WithName("Teste")
          .WithTags("Clientes");

    }
}

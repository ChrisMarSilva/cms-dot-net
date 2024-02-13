using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rinha.Backend._2024.API.Context;
using Rinha.Backend._2024.API.Models.Dtos;
using Rinha.Backend._2024.API.Models.Read;
using Rinha.Backend._2024.API.Models.Write;

namespace Rinha.Backend._2024.API.Endpoints;

public static class ClientesEndpoints
{
    public static void UseMapClientesEndpoints(this WebApplication app)
    {
        app.MapPost("/clientes/{id:int}/transacoes", async (short id, [FromBody] TransacaoRequestDto request, [FromServices] AppWriteDbContext contextWrite, [FromServices] AppReadDbContext contextRead) =>
        {
            try
            {
                //var cliente = await contextRead.Clientes
                //    .AsNoTracking()
                //    .FirstOrDefaultAsync(x => x.IdCliente == id);

                var cliente = await contextRead.Clientes
                   .AsNoTracking()
                   .Where(x => x.IdCliente == id)
                   .Select(x => new ClienteReadModel { IdCliente = x.IdCliente, Limite = x.Limite })
                   .FirstOrDefaultAsync();

                if (cliente is null) return Results.NotFound("Cliente não localizado.");

                //var carteira = await contextWrite.Carteiras
                //    .FirstOrDefaultAsync(x => x.IdCliente == id);

                var carteira = await contextWrite.Carteiras
                    .Where(x => x.IdCliente == id)
                    .Select(x => new ClienteCarteiraWriteModel { IdCliente = x.IdCliente, Saldo = x.Saldo })
                    .FirstOrDefaultAsync();

                if (carteira is null) return Results.NotFound("Carteira do Cliente não localizado.");

                if (request.Tipo!.Equals("d", StringComparison.OrdinalIgnoreCase))
                {
                    var novoSado = Math.Abs(carteira.Saldo - request.Valor!.Value);
                    if (cliente.Limite < novoSado) return Results.UnprocessableEntity("Novo saldo do cliente menor que seu limite disponível."); 

                    carteira.Saldo -= request.Valor!.Value;
                }
                else if (request.Tipo!.Equals("C", StringComparison.OrdinalIgnoreCase))
                {
                    carteira.Saldo += request.Valor!.Value;
                }
                else 
                {
                    return Results.UnprocessableEntity("Tipo de transação inválido."); 
                }

                var transacao = new ClienteTransacaoWriteModel( 
                    idCliente: cliente.IdCliente, 
                    valor: request.Valor!.Value,
                    tipo: request.Tipo.ToLower(), 
                    descricao: request.Descricao!);

                using (var transaction = await contextWrite.Database.BeginTransactionAsync()) //await using var transaction = await _pagadorCommandRepository.BeginTransactionAsync();
                {
                    try
                    {
                        contextWrite.Carteiras.Update(carteira);
                        await contextWrite.Transacoes.AddAsync(transacao);

                        await contextWrite.SaveChangesAsync(); //await _pagadorCommandRepository.SaveChangesAsync();
                        await transaction.CommitAsync();
                    }
                    catch
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }

                var response = new TransacaoResponseDto(Limite: cliente.Limite, Saldo: carteira.Saldo);
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
                //var cliente = await contextRead.Clientes
                //    .AsNoTracking()
                //    .FirstOrDefaultAsync(x => x.IdCliente == id);

                var cliente = await contextRead.Clientes
                    .AsNoTracking()
                    .Where(x => x.IdCliente == id)
                    .Select(x => new ClienteReadModel { IdCliente = x.IdCliente, Limite = x.Limite })
                    .FirstOrDefaultAsync();

                if (cliente is null) return Results.NotFound("Cliente não localizado.");

                //var carteira = await contextRead.Carteiras
                //    .AsNoTracking()
                //    .FirstOrDefaultAsync(x => x.IdCliente == id);

                var carteira = await contextRead.Carteiras
                    .AsNoTracking()
                    .Where(x => x.IdCliente == id)
                    .Select(x => new ClienteCarteiraReadModel { IdCliente = x.IdCliente, Saldo = x.Saldo })
                    .FirstOrDefaultAsync();

                if (carteira is null) return Results.NotFound("Carteira do Cliente não localizado.");

                var transacoes = await contextRead.Transacoes
                  .AsNoTracking()
                  .Where(x => x.IdCliente == id)
                  .OrderByDescending(c => c.DtHrRegistro)
                  .Take(10)
                  .Select(x => new ClienteTransacaoReadModel { IdTransacao = x.IdTransacao, IdCliente = x.IdCliente, Valor = x.Valor, Tipo = x.Tipo, Descricao = x.Descricao, DtHrRegistro = x.DtHrRegistro })
                  //.Select(x => new ExtratoTransacoesResponseDto { valor = x.Valor, tipo = x.Tipo, descricao = x.Descricao, realizada_em = x.DtHrRegistro.ToString("yyyy-MM-ddTHH:mm:ss.ffffffZ", System.Globalization.CultureInfo.InvariantCulture) })
                  .ToListAsync();

                //if (transacoes is null || !transacoes.Any()) return Results.NotFound("Transações do Cliente não localizada.");

                var response = new ExtratoResponseDto(
                    saldo: new ExtratoSaldoResponseDto(total: carteira.Saldo, data_extrato: DateTime.Now, limite: cliente.Limite), 
                    Transacoes: transacoes is null || !transacoes.Any() ? null : transacoes.Select(x => new ExtratoTransacoesResponseDto(valor: x.Valor, tipo: x.Tipo, descricao: x.Descricao, realizada_em: x.DtHrRegistro.ToString("yyyy-MM-ddTHH:mm:ss.ffffffZ", System.Globalization.CultureInfo.InvariantCulture))).ToList()
                );

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

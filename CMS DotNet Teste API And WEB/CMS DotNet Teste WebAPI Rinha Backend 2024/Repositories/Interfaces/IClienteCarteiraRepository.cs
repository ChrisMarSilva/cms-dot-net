using System.Data;

namespace Rinha.Backend._2024.API.Repositories.Interfaces;

public interface IClienteCarteiraRepository
{
    Task<long> GetSaldoAsync(int idCliente);
    Task<bool> UpdateSaldoAsync(int idCliente, string tipo, long valor, IDbTransaction? transaction = null);
}

using Rinha.Backend._2024.API.Dtos;
using System.Data;

namespace Rinha.Backend._2024.API.Repositories.Interfaces;

public interface IClienteRepository
{
    Task<ClienteDto?> GetByIdAsync(int id);
    Task<long> GetLimiteAsync(int id);
    Task<long> GetSaldoAsync(int id);
    Task<bool> UpdateSaldoAsync(int id, string tipo, long valor, IDbTransaction? transaction = null);
}

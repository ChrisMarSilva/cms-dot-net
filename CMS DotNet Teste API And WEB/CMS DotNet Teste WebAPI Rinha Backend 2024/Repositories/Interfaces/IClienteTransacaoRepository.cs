using Rinha.Backend._2024.API.Dtos;
using System.Data;

namespace Rinha.Backend._2024.API.Repositories.Interfaces;

public interface IClienteTransacaoRepository
{
    Task<IEnumerable<ClienteTransacaoDto>> GetAllAsync(int idCliente);
    Task<bool> AddAsync(int idCliente, long valor, string tipo, string descricao, IDbTransaction? transaction = null);
}

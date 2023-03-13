using Tarefas.Domain.Models;

namespace Tarefas.Service.Interfaces;

public interface ITarefaService
{
    Task<IEnumerable<Tarefa>> GetAllAsync();
    Task<Tarefa> GetByIdAsync(Guid id);
    Task<Tarefa> InsertAsync(Tarefa input);
    Task<Tarefa> UpdateAsync(Guid id, Tarefa input);
    Task<bool> DeleteAsync(Guid id);
}

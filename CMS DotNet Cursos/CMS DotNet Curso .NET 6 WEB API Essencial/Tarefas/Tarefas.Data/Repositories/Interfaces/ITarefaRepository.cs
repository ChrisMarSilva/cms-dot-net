using Tarefas.Domain.Models;

namespace Tarefas.Data.Repositories.Interfaces;

public interface ITarefaRepository
{
    Task<IEnumerable<Tarefa>> FindAllAsync();
    Task<Tarefa> FindByIdAsync(Guid id);
    Task<Tarefa> CreateAsync(Tarefa input);
    Tarefa Update(Tarefa input);
    bool Delete(Tarefa input);
}

namespace Tarefas.Data.Persistence.Interfaces;

public interface IUnitofWork
{
    Task<bool> CommitAsync();
    Task RollbackAsync();
}

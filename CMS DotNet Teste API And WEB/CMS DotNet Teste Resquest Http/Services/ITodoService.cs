using HttpClientFactoryProject.Models;

namespace HttpClientFactoryProject.Services;

public interface ITodoService
{
    Task<TodoModel?> GetByIdAsync(int id);
    Task<IEnumerable<TodoModel>?> GetAllAsync(); // IList // IEnumerable
    Task<IEnumerable<TodoModel>?> GetTodosAsync();
}

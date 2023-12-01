using HttpClientFactoryProject.Models;
using Refit;

namespace HttpClientFactoryProject.Abstractions;

public interface ITodoApi
{
    [Get("/todos")]
    Task<IEnumerable<TodoModel>?> ReturnTodo();
}
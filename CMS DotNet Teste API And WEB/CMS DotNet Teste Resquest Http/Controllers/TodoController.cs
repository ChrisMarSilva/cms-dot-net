using HttpClientFactoryProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace HttpClientFactoryProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    private readonly ITodoService _todoService;

    public TodoController(ITodoService todoService)
    {
        _todoService = todoService;
    }

    [HttpGet("GetTodo/{id}")]
    public async Task<ActionResult> GetTodo(int id)
    {
        var result = await _todoService.GetByIdAsync(id: id);

        return Ok(result);
    }

    [HttpGet("GetTodo")]
    public async Task<ActionResult> GetTodoAll()
    {
        var results = await _todoService.GetAllAsync();

        return Ok(results);
    }

    [HttpGet("GetTodos")]
    public async Task<ActionResult> GetTodos()
    {
        var results = await _todoService.GetTodosAsync();

        return Ok(results);
    }
}

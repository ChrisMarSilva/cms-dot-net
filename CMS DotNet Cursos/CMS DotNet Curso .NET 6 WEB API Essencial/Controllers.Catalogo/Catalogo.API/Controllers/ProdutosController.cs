using Catalogo.Domain.Models;
using Catalogo.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Catalogo.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly ILogger<ProdutosController> _logger;
    private readonly IProdutoService _prodService;
    private readonly string? _className;

    public ProdutosController(
        ILogger<ProdutosController> logger,
        IProdutoService prodService
        )
    {
        _logger = logger;
        _prodService = prodService ?? throw new ArgumentNullException(nameof(ProdutosController));
        _className = GetType().FullName;

        _logger.LogInformation($"{_className}");
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Produto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll() // Task<ActionResult<IEnumerable<Produto>>>
    {
        _logger.LogInformation($"{_className}.GetAll()");
        try
        {
            var results = await _prodService.GetAllAsync();

            if (results is null || !results.Any())
                return NotFound("No records found");

            return Ok(results);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{_className}.GetAll(Erro: {ex.Message})");
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    [HttpGet("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Produto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(Guid id) // Task<ActionResult<Produto>>
    {
        _logger.LogInformation($"{_className}.GetById()");
        try
        {
            var result = await _prodService.GetByIdAsync(id);

            if (result is null || result?.Id == Guid.Empty)
                return NotFound("No record found");

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{_className}.GetById(Erro: {ex.Message})");
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(Produto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post(Produto input)
    {
        _logger.LogInformation($"{_className}.Post()");
        try
        {
            var result = await _prodService.InsertAsync(input);

            if (result is null || result?.Id == Guid.Empty)
                return BadRequest();

            return CreatedAtAction(nameof(GetById), new { id = result?.Id }, result);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{_className}.Post(Erro: {ex.Message})");
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    [HttpPut("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(Guid id, Produto input)
    {
        _logger.LogInformation($"{_className}.Update()");
        try
        {
            var result = await _prodService.UpdateAsync(id, input);

            if (result is null || result?.Id == Guid.Empty)
                return NotFound("No records found");

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{_className}.Update(Erro: {ex.Message})");
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    [HttpDelete("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation($"{_className}.Delete()");
        try
        {
            var result = await _prodService.DeleteAsync(id);

            if (!result)
                return NotFound("No records found");

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{_className}.Delete(Erro: {ex.Message})");
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }
}

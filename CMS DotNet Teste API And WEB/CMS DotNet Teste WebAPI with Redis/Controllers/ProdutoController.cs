using CMS_DotNet_Teste_WebAPI_with_Redis.Dtos;
using CMS_DotNet_Teste_WebAPI_with_Redis.Services;
using Microsoft.AspNetCore.Mvc;

namespace CMS_DotNet_Teste_WebAPI_with_Redis.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Consumes("application/json")]
public class ProdutoController : ControllerBase
{
    private readonly ILogger<ProdutoController> _logger;
    private readonly IProdutoService _prodService;

    public ProdutoController(ILogger<ProdutoController> logger, IProdutoService prodService)
    {
        _logger = logger;
        _prodService = prodService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProdutoResponseDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var response = await _prodService.GetAllAsync();

            if (response is null || !response.Any())
                return NotFound("No records found");

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    [HttpGet("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProdutoResponseDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var response = await _prodService.GetByIdAsync(id);

            if (response is null || response?.Id == Guid.Empty)
                return NotFound("No record found");

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(ProdutoResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post(ProdutoRequestDto request)
    {
        try
        {
            var response = await _prodService.InsertAsync(request);

            if (response is null || response?.Id == Guid.Empty)
                return BadRequest();

            return CreatedAtAction(nameof(GetById), new { id = response?.Id }, response);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    [HttpPut("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(Guid id, ProdutoRequestDto request)
    {
        try
        {
            var response = await _prodService.UpdateAsync(id, request);

            if (response is null || response?.Id == Guid.Empty)
                return NotFound("No records found");

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    [HttpDelete("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var response = await _prodService.DeleteAsync(id);

            if (!response)
                return NotFound("No records found");

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }
}

using Catalogo.Application.Dtos;
using Catalogo.Application.Interfaces;
using Catalogo.Domain.Pagination;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using System.Text.Json;

namespace Catalogo.API.Controllers.v1;

// https://localhost:7176/api/v1/produtos?$select=nome,preco&$orderby=preco,nome&$filter=preco%20lt%207

[ApiController]
[ApiVersion("1")]
[EnableQuery]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
[Consumes("application/json")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ProdutosController : ControllerBase
{
    private readonly ILogger<ProdutosController> _logger;
    private readonly IProdutoService _prodService;
    private readonly string _className;

    public ProdutosController(ILogger<ProdutosController> logger, IProdutoService prodService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _prodService = prodService ?? throw new ArgumentNullException(nameof(IProdutoService));
        _className = GetType().FullName;

        _logger.LogInformation($"{_className}");
    }

    /// <summary>
    /// Exibe uma relação dos produtos
    /// </summary>
    /// <returns>Retorna uma lista de objetos Produto</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProdutoResponseDTO>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll([FromQuery] ProdutosParameters prodParams) // Task<ActionResult<IEnumerable<ProdutoRequestDTO>>>
    {
        _logger.LogInformation($"{_className}.GetAll()");
        try
        {
            var (metadata, response) = await _prodService.GetAllAsync(prodParams);

            if (response is null || !response.Any())
                return NotFound("No records found");

            //var metadata = new { results.TotalCount, results.PageSize, results.CurrentPage, results.TotalPages, results.HasNext, results.HasPrevious };
            // Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{_className}.GetAll(Erro: {ex.Message})");
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }
    /// <summary>
    /// Obtem um produto pelo seu identificador produtoId
    /// </summary>
    /// <param name="id">Código do produto</param>
    /// <returns>Um objeto Produto</returns>
    [HttpGet("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProdutoResponseDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(Guid id) // Task<ActionResult<ProdutoRequestDTO>>
    {
        _logger.LogInformation($"{_className}.GetById()");
        try
        {
            var response = await _prodService.GetByIdAsync(id);

            if (response is null || response?.Id == Guid.Empty)
                return NotFound("No record found");

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{_className}.GetById(Erro: {ex.Message})");
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(ProdutoResponseDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post(ProdutoRequestDTO request)
    {
        _logger.LogInformation($"{_className}.Post()");
        try
        {
            var response = await _prodService.InsertAsync(request);

            if (response is null || response?.Id == Guid.Empty)
                return BadRequest();

            return CreatedAtAction(nameof(GetById), new { id = response?.Id }, response);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{_className}.Post(Erro: {ex.Message})");
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }
    /// <summary>
    /// Atualiza um produto pelo id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(Guid id, ProdutoRequestDTO request)
    {
        _logger.LogInformation($"{_className}.Update()");
        try
        {
            var response = await _prodService.UpdateAsync(id, request);

            if (response is null || response?.Id == Guid.Empty)
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
            var response = await _prodService.DeleteAsync(id);

            if (!response)
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

using Catalogo.Data.Pagination;
using Catalogo.Domain.Dtos;
using Catalogo.Domain.Models;
using Catalogo.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using System.Text.Json;

namespace Catalogo.API.Controllers.v1;

//[ApiConventionType(typeof(DefaultApiConventions))]
[EnableQuery]
[Produces("application/json")]
[Route("api/v1/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class AlunosController : ControllerBase
{

    private readonly ILogger<AlunosController> _logger;
    private readonly IAlunoService _alunoService;
    private readonly string _className;

    //public AlunosController(IAlunoService alunoService)
    //{
    //    _alunoService = alunoService ?? throw new ArgumentNullException(nameof(IAlunoService));
    //    _className = GetType().FullName;
    //}

    public AlunosController(
        ILogger<AlunosController> logger,
        IAlunoService alunoService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _alunoService = alunoService ?? throw new ArgumentNullException(nameof(IAlunoService));
        _className = GetType().FullName;

        _logger.LogInformation($"{_className}");
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AlunoResponseDTO>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IAsyncEnumerable<Aluno>>> GetAll([FromQuery] AlunosParameters alunoParams)
    {
        // _logger.LogInformation($"{_className}.GetAll()");
        try
        {
            var (metadata, response) = await _alunoService.GetAllAsync(alunoParams);

            if (response is null || !response.Any())
                return NotFound("No records found");

            // var metadata = new { results.TotalCount, results.PageSize, results.CurrentPage, results.TotalPages, results.HasNext, results.HasPrevious };
            // Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));


            return Ok(response);
        }
        catch (Exception ex)
        {
            //_logger.LogError($"{_className}.GetAll(Erro: {ex.Message})");
            // _logger.LogError(ex, $"{_className}.GetAll(Erro: {ex.Message})");
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    [HttpGet("AlunoPorNome")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AlunoResponseDTO>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IAsyncEnumerable<Aluno>>> GetAlunoPorNome([FromQuery] string nome)
    {
        // _logger.LogInformation($"{_className}.GetAll()");
        try
        {
            var response = await _alunoService.GetByNomeAsync(nome);

            if (response is null || !response.Any())
                return NotFound("Não existem alunos com nome = {nome}");

            return Ok(response);
        }
        catch (Exception ex)
        {
            //_logger.LogError($"{_className}.GetAll(Erro: {ex.Message})");
            // _logger.LogError(ex, $"{_className}.GetAll(Erro: {ex.Message})");
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    /// <summary>
    /// Obtem um Aluno pelo seu Id
    /// </summary>
    /// <param name="id">codigo do aluno</param>
    /// <returns>Objeto Aluno</returns>
    [HttpGet("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlunoResponseDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(Guid id) // Task<ActionResult<AlunoRequestDTO>>
    {
        // _logger.LogInformation($"{_className}.GetById()");
        try
        {
            var response = await _alunoService.GetByIdAsync(id);

            if (response is null || response?.Id == Guid.Empty)
                return NotFound("No record found");

            return Ok(response);
        }
        catch (Exception ex)
        {
            // _logger.LogError($"{_className}.GetById(Erro: {ex.Message})");
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    /// <summary>
    /// Inclui um novo aluno
    /// </summary>
    /// <remarks>
    /// Exemplo de request:
    ///
    ///     POST api/alunos
    ///     {
    ///        "id": 1,
    ///        "nome": "aluno1",
    ///        "email": "aluno@gamil.com",
    ///        "idade": 10
    ///     }
    /// </remarks>
    /// <param name="request">objeto Aluno</param>
    /// <returns>O objeto Aluno incluido</returns>
    /// <remarks>Retorna um objeto Aluno incluído</remarks>
    [HttpPost]
    [ProducesResponseType(typeof(AlunoResponseDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post(AlunoRequestDTO request)
    {
        // _logger.LogInformation($"{_className}.Post()");
        try
        {
            var response = await _alunoService.InsertAsync(request);

            if (response is null || response?.Id == Guid.Empty)
                return BadRequest();

            return CreatedAtAction(nameof(GetById), new { id = response?.Id }, response);
        }
        catch (Exception ex)
        {
            // _logger.LogError($"{_className}.Post(Erro: {ex.Message})");
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    [HttpPut("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //[ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
    public async Task<IActionResult> Update(Guid id, AlunoRequestDTO request)
    {
        // _logger.LogInformation($"{_className}.Update()");
        try
        {
            var response = await _alunoService.UpdateAsync(id, request);

            if (response is null || response?.Id == Guid.Empty)
                return NotFound("No records found");

            return NoContent();
        }
        catch (Exception ex)
        {
            // _logger.LogError($"{_className}.Update(Erro: {ex.Message})");
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    [HttpDelete("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(Guid id)
    {
        // _logger.LogInformation($"{_className}.Delete()");
        try
        {
            var response = await _alunoService.DeleteAsync(id);

            if (!response)
                return NotFound("No records found");

            return NoContent();
        }
        catch (Exception ex)
        {
            // _logger.LogError($"{_className}.Delete(Erro: {ex.Message})");
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }
}

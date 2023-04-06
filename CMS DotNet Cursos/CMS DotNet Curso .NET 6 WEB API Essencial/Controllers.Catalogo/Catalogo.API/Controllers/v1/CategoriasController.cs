using Catalogo.Data.Pagination;
using Catalogo.Domain.Dtos;
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
public class CategoriasController : ControllerBase // : BaseController<CategoriasController>
{
    private readonly ILogger<CategoriasController> _logger;
    private readonly ICategoriaService _categService;
    private readonly string _className;

    //public CategoriasController(ICategoriaService categService)
    //{
    //    _categService = categService ?? throw new ArgumentNullException(nameof(ICategoriaService));
    //    _className = GetType().FullName;
    //}

    public CategoriasController(
        ILogger<CategoriasController> logger, 
        ICategoriaService categService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _categService = categService ?? throw new ArgumentNullException(nameof(ICategoriaService));
        _className = GetType().FullName;

        _logger.LogInformation($"{_className}");
    }

    //[AllowAnonymous]
    //[HttpGet("teste")]
    //public string GeTeste()
    //{
    //    return $"CategoriasController - {DateTime.Now.ToLongDateString().ToString()}";
    //}

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CategoriaResponseDTO>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll([FromQuery] CategoriasParameters categParams) // Task<ActionResult<IEnumerable<CategoriaRequestDTO>>>
    {
        // _logger.LogInformation($"{_className}.GetAll()");
        try
        {
            var (metadata, response) = await _categService.GetAllAsync(categParams);

            if (response is null || !response.Any())
                return NotFound("No records found");

            //var metadata = new { results.TotalCount, results.PageSize, results.CurrentPage, results.TotalPages, results.HasNext, results.HasPrevious };
            //Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
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

    [HttpGet("paginacao")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CategoriaResponseDTO>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPaginacao(int pag = 1, int reg = 5) 
    {
        // _logger.LogInformation($"{_className}.GetAll()");
        try
        {
            var (totalDeRegistros, numeroPaginas, response) = await _categService.GetPaginacaoAsync(pag, reg);

            if (response is null || !response.Any())
                return NotFound("No records found");

            Response.Headers["X-Total-Registros"] = totalDeRegistros.ToString();
            Response.Headers["X-Numero-Paginas"] = numeroPaginas.ToString();

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
    /// Obtem uma Categoria pelo seu Id
    /// </summary>
    /// <param name="id">codigo do categoria</param>
    /// <returns>Objetos Categoria</returns>
    [HttpGet("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoriaResponseDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(Guid id) // Task<ActionResult<CategoriaRequestDTO>>
    {
        // _logger.LogInformation($"{_className}.GetById()");
        try
        {
            var response = await _categService.GetByIdAsync(id);

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
    /// Inclui uma nova categoria
    /// </summary>
    /// <remarks>
    /// Exemplo de request:
    ///
    ///     POST api/categorias
    ///     {
    ///        "categoriaId": 1,
    ///        "nome": "categoria1",
    ///        "imagemUrl": "http://teste.net/1.jpg"
    ///     }
    /// </remarks>
    /// <param name="request">objeto Categoria</param>
    /// <returns>O objeto Categoria incluida</returns>
    /// <remarks>Retorna um objeto Categoria incluído</remarks>
    [HttpPost]
    [ProducesResponseType(typeof(CategoriaResponseDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post(CategoriaRequestDTO request)
    {
        // _logger.LogInformation($"{_className}.Post()");
        try
        {
            var response = await _categService.InsertAsync(request);

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
    public async Task<IActionResult> Update(Guid id, CategoriaRequestDTO request)
    {
        // _logger.LogInformation($"{_className}.Update()");
        try
        {
            var response = await _categService.UpdateAsync(id, request);

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
            var response = await _categService.DeleteAsync(id);

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

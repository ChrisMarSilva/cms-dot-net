using Catalogo.Domain.Dtos;
using Catalogo.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace Catalogo.API.Controllers.v1;

[ApiController]
[ApiVersion("1")]
[EnableQuery]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
[Consumes("application/json")]
[AllowAnonymous]
public class AutorizaController : ControllerBase
{
    private readonly ILogger<AutorizaController> _logger;
    private readonly IAutorizaService _authService;
    private readonly string _className;

    public AutorizaController(
        ILogger<AutorizaController> logger,
        IAutorizaService authService)
    {
        _logger = logger;
        _authService = authService ?? throw new ArgumentNullException(nameof(IAutorizaService));
        _className = GetType().FullName;

        _logger.LogInformation($"{_className}");
    }

    [HttpGet]
    public ActionResult<string> Get()
    {
        _logger.LogInformation($"{_className}.Get()");
        try
        {
            return $"AutorizaController ::  Acessado em: {DateTime.Now.ToLongDateString()}";
        }
        catch (Exception ex)
        {
            //_logger.LogError($"{_className}.Get(Erro: {ex.Message})");
            _logger.LogError(ex, $"{_className}.Get(Erro: {ex.Message})");
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    /// <summary>
    /// Registra um novo usuário
    /// </summary>
    /// <param name="request">Um objeto UsuarioRequestDTO</param>
    /// <returns>Status 200 e o token para o cliente</returns>
    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] UsuarioRequestDTO request)
    {
        _logger.LogInformation($"{_className}.Register()");
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            var response = await _authService.RegisterAsync(request);

            if (response is null)
                return BadRequest();

            return Ok(response);
        }
        catch (Exception ex)
        {
            //_logger.LogError($"{_className}.Register(Erro: {ex.Message})");
            _logger.LogError(ex, $"{_className}.Register(Erro: {ex.Message})");
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    /// <summary>
    /// Verifica as credenciais de um usuário
    /// </summary>
    /// <param name="request">Um objeot do tipo LoginRequestDTO</param>
    /// <returns>Status 200 e o token para o cliente</returns>
    /// <remarks>retorna o Status 200 e o token para  novo</remarks>
    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginRequestDTO request)
    {
        _logger.LogInformation($"{_className}.Login()");
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            var response = await _authService.LoginAsync(request);

            if (response is null)
                return BadRequest();

            return Ok(response);
        }
        catch (Exception ex)
        {
            //_logger.LogError($"{_className}.Login(Erro: {ex.Message})");
            _logger.LogError(ex, $"{_className}.Login(Erro: {ex.Message})");
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    /// <summary>
    /// Realiza logout de um usuário
    /// </summary>
    /// <returns>Status 200</returns>
    /// <remarks>retorna o Status 200</remarks>
    [HttpPost("logout")]
    public async Task<ActionResult> Logout()
    {
        _logger.LogInformation($"{_className}.Logout()");
        try
        {
            await _authService.LogoutAsync();

            return Ok();
        }
        catch (Exception ex)
        {
            //_logger.LogError($"{_className}.Logout(Erro: {ex.Message})");
            _logger.LogError(ex, $"{_className}.Logout(Erro: {ex.Message})");
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }
}

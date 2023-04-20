using Catalogo.API.Filters;
using Catalogo.Application.Interfaces;
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
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class MeuController : ControllerBase
{
    private readonly ILogger<MeuController> _logger;
    private readonly IConfiguration _config;
    private readonly IMeuServico _meuService;
    private readonly string _className;

    public MeuController(
        ILogger<MeuController> logger,
        IConfiguration config,
        IMeuServico meuService
        )
    {
        _logger = logger;
        _config = config;
        _meuService = meuService ?? throw new ArgumentNullException(nameof(meuService));
        _className = GetType().FullName;

        _logger.LogInformation($"{_className}");
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<string>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ServiceFilter(typeof(ApiLoggingFilter))]
    public ActionResult<string> Get(string nome)
    {
        _logger.LogInformation($"{_className}.Get(INI)");
        try
        {
            return _meuService.Saudacao(nome);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{_className}.Get(ERROR): {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
        finally
        {
            _logger.LogInformation($"{_className}.Get(FIM)");
        }
    }

    [HttpGet("saudacao/{nome=BBB}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<string>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<string> GetSaudacao([FromServices] IMeuServico meuService, string nome = "BBB")
    {
        _logger.LogInformation($"{_className}.GetSaudacao(INI)");
        try
        {
            return meuService.Saudacao(nome);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{_className}.GetSaudacao(ERROR): {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
        finally
        {
            _logger.LogInformation($"{_className}.GetSaudacao(FIM)");
        }
    }

    [HttpGet("autor")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<string>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<string> GetAutor()
    {
        _logger.LogInformation($"{_className}.GetAutor(INI)");
        try
        {
            var autor = _config["Autor"];
            //  var conexao = _config["ConnectionStrings:DefaultConnection"];
            return $"Autor : {autor}"; // return $"Autor : {autor}  Conexao: {conexao}";
        }
        catch (Exception ex)
        {
            _logger.LogError($"{_className}.GetAutor(ERROR): {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
        finally
        {
            _logger.LogInformation($"{_className}.GetAutor(FIM)");
        }
    }

    [HttpGet("erro")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<string>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<string> GetError()
    {
        throw new Exception("Exception ao retornar produto pelo id");
    }
}

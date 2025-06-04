using Cache.App.Api.Services;
using Cache.Contracts.Request;
using Cache.Contracts.Response;
using Cache.Domain.Models;
using Cache.Infra.Bootstrap.Filters;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Cache.Api.Controllers.v1;

[ApiController]
//[ApiVersion("1")]
[Route("api/v1/[controller]")]
//[Route("api/v{version:apiVersion}")]
[Produces("application/json")]
[Consumes("application/json")]
//[ControllerName("1 - User")]
//[Authorize(policy: "api")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserService _userService;
    private IValidator<UserRequestDto> _validator;

    public UserController(ILogger<UserController> logger, IUserService userService, IValidator<UserRequestDto> validator)
    {
        _logger = logger;
        _userService = userService;
        _validator = validator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserResponseDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponseDto))]
    [ProducesResponseType(StatusCodes.Status451UnavailableForLegalReasons, Type = typeof(ErrorResponseDto))]
    [ProducesDefaultResponseType(typeof(ErrorResponseDto))]
    [ValidateIdempotencyKeyFilter(cacheTimeInMinutes: 60)]
    //[ServiceFilter(typeof(ValidateIdempotencyKeyFilterAttribute))]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        try
        {
            var usersModel = await _userService.GetAllAsync(cancellationToken);

            var response = usersModel.MapToResponse();
            // return Ok(response);

            var idempotencyResult = Ok(response);
            HttpContext.Items["idempotency-response-body"] = idempotencyResult;
            return idempotencyResult;
        }
        catch (OperationCanceledException e)
        {
            _logger.LogError(e, "Operação cancelada pelo sistema cliente.");
            return StatusCode(StatusCodes.Status504GatewayTimeout);
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Falha geral, segue a descrição: {description}.", ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ErrorResponseDto.Iniciar(HttpStatusCode.InternalServerError, "Falha interna durante o processamente. Favor tentar novamente"));
        }
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserResponseDto))]
    [ProducesResponseType(StatusCodes.Status102Processing)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponseDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseDto))]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesDefaultResponseType(typeof(ErrorResponseDto))]
    //[ValidateIdempotencyKeyFilterAttribute]
    public async Task<IActionResult> FindById(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var userModel = await _userService.GetByIdAsync(id, cancellationToken);

            if (userModel is null)
                return NotFound(new ErrorResponseDto(HttpStatusCode.NotFound, "Nenhuma registro encontrado com esse id."));

            var response = userModel.MapToResponse();
            // return Ok(response);

            var idempotencyResult = Ok(response);
            HttpContext.Items["idempotency-response-body"] = idempotencyResult;
            return idempotencyResult;
        }
        catch (OperationCanceledException e)
        {
            _logger.LogError(e, "Operação cancelada pelo sistema cliente.");
            return StatusCode(StatusCodes.Status504GatewayTimeout);
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Falha geral, segue a descrição: {description}.", ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ErrorResponseDto.Iniciar(HttpStatusCode.InternalServerError, "Falha interna durante o processamente. Favor tentar novamente"));
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(UserResponseDto))]
    [ProducesResponseType(StatusCodes.Status102Processing)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponseDto))]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesDefaultResponseType(typeof(ErrorResponseDto))]
    [ValidateIdempotencyKeyFilter]
    public async Task<IActionResult> Create([FromBody] UserRequestDto request, [FromHeader(Name = "Idempotency-Key")] string idempotenceKey, CancellationToken cancellationToken = default)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ErrorResponseDto.Iniciar(validationResult.Errors));

            var userModel = request.MapToUserModel();
            var result = await _userService.CreateAsync(userModel, cancellationToken);
            var response = result.MapToResponse();
            // return Created(nameof(FindById), response);

            var idempotencyResult = AcceptedAtAction(nameof(FindById), new { response.Id }, response);
            HttpContext.Items["idempotency-response-body"] = idempotencyResult;
            return idempotencyResult;
        }
        catch (OperationCanceledException e)
        {
            _logger.LogError(e, "Operação cancelada pelo sistema cliente.");
            return StatusCode(StatusCodes.Status504GatewayTimeout);
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Falha geral, segue a descrição: {description}.", ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ErrorResponseDto.Iniciar(HttpStatusCode.InternalServerError, "Falha interna durante o processamente. Favor tentar novamente"));
        }
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(UserResponseDto))]
    [ProducesResponseType(StatusCodes.Status102Processing)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponseDto))]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesDefaultResponseType(typeof(ErrorResponseDto))]
    [ValidateIdempotencyKeyFilter]
    public async Task<IActionResult> Update([FromBody] UserRequestDto request, [FromHeader(Name = "Idempotency-Key")] string idempotenceKey, Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ErrorResponseDto.Iniciar(validationResult.Errors));

            var userModel = await _userService.GetByIdAsync(id, cancellationToken);
            if (userModel is null || userModel.Id == Guid.Empty)
                return NotFound(new ErrorResponseDto(HttpStatusCode.NotFound, "Nenhuma registro encontrado com esse id."));

            var userRequest = request.MapToUserModel();

            userModel.AlterUser(userRequest.Name, userRequest.Email, userRequest.Password);

            var result = await _userService.UpdateAsync(userModel, cancellationToken);
            var response = result!.MapToResponse();
            // return Ok(response);

            var idempotencyResult = AcceptedAtAction(nameof(FindById), new { response.Id }, response);
            HttpContext.Items["idempotency-response-body"] = idempotencyResult;
            return idempotencyResult;
        }
        catch (OperationCanceledException e)
        {
            _logger.LogError(e, "Operação cancelada pelo sistema cliente.");
            return StatusCode(StatusCodes.Status504GatewayTimeout);
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Falha geral, segue a descrição: {description}.", ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ErrorResponseDto.Iniciar(HttpStatusCode.InternalServerError, "Falha interna durante o processamente. Favor tentar novamente"));
        }
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(UserResponseDto))]
    [ProducesResponseType(StatusCodes.Status102Processing)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponseDto))]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesDefaultResponseType(typeof(ErrorResponseDto))]
    [ValidateIdempotencyKeyFilter]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var userModel = await _userService.GetByIdAsync(id, cancellationToken);

            if (userModel is null)
                return NotFound(new ErrorResponseDto(HttpStatusCode.NotFound, "Nenhuma registro encontrado com esse id."));

            await _userService.DeleteByIdAsync(userModel, cancellationToken);
            //return NoContent();

            var idempotencyResult = NoContent();
            HttpContext.Items["idempotency-response-body"] = idempotencyResult;
            return idempotencyResult;
        }
        catch (OperationCanceledException e)
        {
            _logger.LogError(e, "Operação cancelada pelo sistema cliente.");
            return StatusCode(StatusCodes.Status504GatewayTimeout);
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Falha geral, segue a descrição: {description}.", ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ErrorResponseDto.Iniciar(HttpStatusCode.InternalServerError, "Falha interna durante o processamente. Favor tentar novamente"));
        }
    }
}


public static class ContraUserMappingctMapping
{
    public static UserModel MapToUserModel(this UserRequestDto request) =>
        new UserModel(
            name: request.Name,
            email: request.Email,
            password: request.Password);

    public static UserResponseDto MapToResponse(this UserModel userModel) =>
        new UserResponseDto(
            id: userModel.Id,
            name: userModel.Name,
            email: userModel.Email,
            password: userModel.Password,
            dtHrCreated: userModel.DtHrCreated);

    public static IEnumerable<UserResponseDto> MapToResponse(this IEnumerable<UserModel> userModel) =>
        userModel.Select(userModel => userModel.MapToResponse());
}
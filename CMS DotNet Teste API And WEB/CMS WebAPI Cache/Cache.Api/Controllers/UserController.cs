using Cache.Api.Contracts.Mappings;
using Cache.Api.Contracts.Requests;
using Cache.Api.Contracts.Responses;
using Cache.Api.Database.Contexts;
using Cache.Api.Filters;
using Cache.Api.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Cache.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserService _userService;
    private IValidator<UserRequestDto> _validator;

    public UserController(ILogger<UserController> logger, IUserService userService, AppDbContext ctx, IValidator<UserRequestDto> validator)
    {
        _logger = logger;
        _userService = userService;
        _validator = validator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserResponseDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErroResponseDto))]
    [ProducesDefaultResponseType(typeof(ErroResponseDto))]
    [ValidateIdempotencyKeyFilterAttribute(cacheTimeInMinutes: 60)]
    //[ServiceFilter(typeof(ValidateIdempotencyKeyFilterAttribute))]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        try
        {
            var usersModel = await _userService.GetAllAsync(cancellationToken);

            var response = usersModel.MapToResponse();
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Falha geral, segue a descrição: {description}.", ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ErroResponseDto.Iniciar(HttpStatusCode.InternalServerError, "Falha interna durante o processamente. Favor tentar novamente"));
        }
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserResponseDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErroResponseDto))]
    [ProducesDefaultResponseType(typeof(ErroResponseDto))]
    [ValidateIdempotencyKeyFilterAttribute]
    public async Task<IActionResult> FindById(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var userModel = await _userService.GetByIdAsync(id, cancellationToken);

            if (userModel is null)
                return NotFound(new ErroResponseDto(HttpStatusCode.NotFound, "Nenhuma registro encontrado com esse id."));

            var response = userModel.MapToResponse();
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Falha geral, segue a descrição: {description}.", ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ErroResponseDto.Iniciar(HttpStatusCode.InternalServerError, "Falha interna durante o processamente. Favor tentar novamente"));
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(UserResponseDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErroResponseDto))]
    [ProducesDefaultResponseType(typeof(ErroResponseDto))]
    [ValidateIdempotencyKeyFilterAttribute]
    public async Task<IActionResult> Create([FromBody] UserRequestDto request, [FromHeader(Name = "Idempotency-Key")] string idempotenceKey, CancellationToken cancellationToken = default)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ErroResponseDto.Iniciar(validationResult.Errors));

            var userModel = request.MapToUserModel();
            var result = await _userService.CreateAsync(userModel, cancellationToken);
            var response = result.MapToResponse();

            var idempotencyResult = AcceptedAtAction(nameof(FindById), new { Id = response.Id }, response);
            HttpContext.Items["idempotency-response-body"] = idempotencyResult;
            //HttpContext.Items[IdempotencyOptions.IdempotencyResponseBodyKey] = idempotencyResult;

            return idempotencyResult;
            // return Created(nameof(FindById), response);
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Falha geral, segue a descrição: {description}.", ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ErroResponseDto.Iniciar(HttpStatusCode.InternalServerError, "Falha interna durante o processamente. Favor tentar novamente"));
        }
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(UserResponseDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErroResponseDto))]
    [ProducesDefaultResponseType(typeof(ErroResponseDto))]
    [ValidateIdempotencyKeyFilterAttribute]
    public async Task<IActionResult> Update([FromBody] UserRequestDto request, [FromHeader(Name = "Idempotency-Key")] string idempotenceKey, Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ErroResponseDto.Iniciar(validationResult.Errors));

            var userModel = await _userService.GetByIdAsync(id, cancellationToken);
            if (userModel is null || userModel.Id == Guid.Empty)
                return NotFound(new ErroResponseDto(HttpStatusCode.NotFound, "Nenhuma registro encontrado com esse id."));

            var userRequest = request.MapToUserModel();

            userModel.AlterUser(userRequest.Name, userRequest.Email, userRequest.Password);

            var result = await _userService.UpdateAsync(userModel, cancellationToken);
            var response = result!.MapToResponse();

            var idempotencyResult = AcceptedAtAction(nameof(FindById), new { Id = response.Id }, response);
            HttpContext.Items["idempotency-response-body"] = idempotencyResult;
            //HttpContext.Items[IdempotencyOptions.IdempotencyResponseBodyKey] = idempotencyResult;

            return idempotencyResult;
            // return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Falha geral, segue a descrição: {description}.", ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ErroResponseDto.Iniciar(HttpStatusCode.InternalServerError, "Falha interna durante o processamente. Favor tentar novamente"));
        }
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(UserResponseDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErroResponseDto))]
    [ProducesDefaultResponseType(typeof(ErroResponseDto))]
    //[ValidateIdempotencyKeyFilterAttribute]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var userModel = await _userService.GetByIdAsync(id, cancellationToken);

            if (userModel is null)
                return NotFound(new ErroResponseDto(HttpStatusCode.NotFound, "Nenhuma registro encontrado com esse id."));

            await _userService.DeleteByIdAsync(userModel, cancellationToken);

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Falha geral, segue a descrição: {description}.", ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ErroResponseDto.Iniciar(HttpStatusCode.InternalServerError, "Falha interna durante o processamente. Favor tentar novamente"));
        }
    }
}

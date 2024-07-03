using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Project.Domain.Dtos.Request;
using Project.Domain.Dtos.Response;
using Project.Service.Interfaces;
using Project.Extensions;
using System.Diagnostics;
using System.Net;

namespace Project.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
[Consumes("application/json")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IService _service;
    private readonly IDistributedCache _cache;
     

    public ProductController(ILogger<ProductController> logger, IService service, IDistributedCache cache)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _service = service ?? throw new ArgumentNullException(nameof(service));
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(ICollection<ProductResponseDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponseDto))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponseDto))]
    [ProducesDefaultResponseType(typeof(ErrorResponseDto))]
    public async Task<ActionResult<ICollection<ProductResponseDto>>> GetAll(CancellationToken cancellationToken)
    {
        _logger.LogInformation("ProductController.GetAll");
        Activity.Current?.SetTag("aaaaa", "bbbbbbbbbbb");
        Activity.Current?.AddBaggage("CustomerRef", "SomeCustomerReference");
        Activity.Current?.AddEvent(new ActivityEvent("sample activity event."));
        Activity.Current?.SetStatus(ActivityStatusCode.Ok);
        try
        {
            var response = await _service.GetAllProductAsync(cancellationToken);

            return response is null ? NotFound("No records found") : Ok(response);
        }
        catch (OperationCanceledException e)
        {
            _logger.LogError(e, "Operação cancelada pelo sistema cliente.");
            return StatusCode(StatusCodes.Status504GatewayTimeout);
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Falha crítica ao gerar a inclusão do terceiro autorizado. Segue a descrição: {description}", e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ErrorResponseDto.Begin(HttpStatusCode.InternalServerError, "Falha interna durante o processamento. Por favor, tente novamente."));
        }
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(ProductResponseDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponseDto))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponseDto))]
    [ProducesDefaultResponseType(typeof(ErrorResponseDto))]
    public async Task<ActionResult<ProductResponseDto>> GetById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var response = _cache.GetOrCreateAsync($"products-{id}", async () =>
            {
                var productFromDb = await _service.GetByIdProductAsync(id, cancellationToken);

                return productFromDb;
            });

            //var response = await _service.GetByIdProductAsync(id, cancellationToken);

            return response is null ? NotFound("No record found") : Ok(response);

            //var command = await _apiService.GetByIdAsync(id, cancellationToken);
            //if (command)
            //{
            //    return command.Result switch
            //    {
            //        TerceiroConsultaResponseDto response => Ok(response),
            //        byte[] bytes => new IdempotencyResult(HttpStatusCode.Accepted, bytes.AsSpan().ToBody()),
            //        _ => NotFound(ErrorResponseDto.Begin(HttpStatusCode.NotFound).AddError("numCtrlReq").AddDescription("Terceiro Autorizado não encontrado.").End())
            //    };
            //}

            //if (command.ValidationResult.Any(x => x.InnerException is InvalidOperationException))
            //    return StatusCode(StatusCodes.Status451UnavailableForLegalReasons, ErrorResponseDto.Begin(HttpStatusCode.UnavailableForLegalReasons, commandConsultarTerceiro.ValidationResult.First(x => x.InnerException is InvalidOperationException).InnerException.Message).End());

            //_logger.LogError("Falha geral ao consultar a situação do terceiro autorizado, segue a descrição: {description}.", commandConsultarTerceiro.ToString());
            //return StatusCode(StatusCodes.Status500InternalServerError, ErrorResponseDto.Begin(HttpStatusCode.InternalServerError, "Falha interna durante o processamento. Por favor, tente novamente."));
        }
        catch (OperationCanceledException e)
        {
            _logger.LogError(e, "Operação cancelada pelo sistema cliente.");
            return StatusCode(StatusCodes.Status504GatewayTimeout);
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Falha crítica ao gerar a inclusão do terceiro autorizado. Segue a descrição: {description}", e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ErrorResponseDto.Begin(HttpStatusCode.InternalServerError, "Falha interna durante o processamento. Por favor, tente novamente."));
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(ProductResponseDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponseDto))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponseDto))]
    [ProducesDefaultResponseType(typeof(ErrorResponseDto))]
    public async Task<ActionResult<ProductResponseDto>> Create([FromBody] ProductRequestDto request, CancellationToken cancellationToken)
    {
        try
        {
            if (!request.IsValid())
                return BadRequest();

            var response = await _service.InsertProductAsync(request, cancellationToken);

            return response is null ? BadRequest() : CreatedAtAction(nameof(GetById), new { id = response.Id }, response);

        }
        catch (OperationCanceledException e)
        {
            _logger.LogError(e, "Operação cancelada pelo sistema cliente.");
            return StatusCode(StatusCodes.Status504GatewayTimeout);
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Falha crítica ao gerar a inclusão do terceiro autorizado. Segue a descrição: {description}", e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ErrorResponseDto.Begin(HttpStatusCode.InternalServerError, "Falha interna durante o processamento. Por favor, tente novamente."));
        }
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(ProductResponseDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponseDto))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponseDto))]
    [ProducesDefaultResponseType(typeof(ErrorResponseDto))]
    public async Task<ActionResult<ProductResponseDto>> Update(int id, [FromBody] ProductRequestDto request, CancellationToken cancellationToken)
    {
        try
        {
            if (!request.IsValid())
                return BadRequest();

            var response = await _service.UpdateProductAsync(id, request, cancellationToken);

            return response is null ? BadRequest() : NoContent();

        }
        catch (OperationCanceledException e)
        {
            _logger.LogError(e, "Operação cancelada pelo sistema cliente.");
            return StatusCode(StatusCodes.Status504GatewayTimeout);
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Falha crítica ao gerar a inclusão do terceiro autorizado. Segue a descrição: {description}", e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ErrorResponseDto.Begin(HttpStatusCode.InternalServerError, "Falha interna durante o processamento. Por favor, tente novamente."));
        }
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponseDto))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponseDto))]
    [ProducesDefaultResponseType(typeof(ErrorResponseDto))]
    public async Task<ActionResult<bool>> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            var status = await _service.DeleteProductAsync(id, cancellationToken);

            return status ? NoContent() : NotFound(new { message = "No record found" });

        }
        catch (OperationCanceledException e)
        {
            _logger.LogError(e, "Operação cancelada pelo sistema cliente.");
            return StatusCode(StatusCodes.Status504GatewayTimeout);
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Falha crítica ao gerar a inclusão do terceiro autorizado. Segue a descrição: {description}", e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ErrorResponseDto.Begin(HttpStatusCode.InternalServerError, "Falha interna durante o processamento. Por favor, tente novamente."));
        }
    }
}

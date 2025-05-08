using Cache.Shared.Core.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Cache.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
[Consumes("application/json")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    //private readonly IMediator _mediatr; // ISender // IMediator 

    public ProductController(ILogger<ProductController> logger)
    {
        _logger = logger;
        //_mediatr = mediatr;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        //var products = await _mediatr.Send(new ProductListQuery());

        //return Ok(products);
        return Ok();
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> FindById(Guid id)
    {
        //var product = await _mediatr.Send(new ProductGetQuery(id));

        //if (product == null)
        //    return NotFound();

        //return Ok(product);
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductCreateCommand command)
    {
        //var productId = await _mediatr.Send(command);

        //if (Guid.Empty == productId)
        //    return BadRequest();

        //await _mediatr.Publish(new ProductCreatedNotification(productId));

        //return Created($"/product/{productId}", new { id = productId });
        return Created($"/product/1", new { id = 1 });
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        // await _mediatr.Send(new ProductDeleteCommand(id));

        return NoContent();
    }
}

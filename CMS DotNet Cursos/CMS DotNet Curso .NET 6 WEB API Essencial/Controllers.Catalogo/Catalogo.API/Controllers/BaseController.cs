using Microsoft.AspNetCore.Mvc;

namespace Catalogo.API.Controllers;

public class BaseController<T> : ControllerBase where T : BaseController<T>
{
    private ILogger<T>? _logger;
    // private IMapper? _mapper;
    // private IMediator? _mediator;

    protected ILogger<T> Logger => _logger ??= HttpContext.RequestServices.GetRequiredService<ILogger<T>>();
    //protected ILogger<T> Logger => _logger ??= HttpContext.RequestServices.GetService<ILogger<T>>();
    //protected IMapper Mapper => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMapper>();
    //protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();

    //    public BaseController(ILogger<T> logger)
    //    {
    //        _logger = logger;
    //    }
}

namespace Project.Filters;

//public class IdempotentFilter<TRequest, TResponse>
//{
//    private readonly ILogger<IdempotentFilter<TRequest, TResponse>> _logger;
//    private readonly IIdempotentService _idempotentService;

//    public IdempotentFilter(ILogger<IdempotentFilter<TRequest, TResponse>> logger, IIdempotentService idempotentService)
//    {
//        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
//        _idempotentService = idempotentService ?? throw new ArgumentNullException(nameof(idempotentService));
//    }

//    public async Task<TResponse> ExecuteAsync(TRequest request, Func<TRequest, Task<TResponse>> next, string idempotencyKey, CancellationToken cancellationToken)
//    {
//        _logger.LogInformation($"IdempotentFilter.ExecuteAsync({request}): {idempotencyKey}");

//        if (!Guid.TryParse(idempotencyKey, out _))
//            throw new ArgumentException("Invalid idempotency key");

//        var exists = await _idempotentService.ExistsAsync(idempotencyKey, cancellationToken);
//        if (exists)
//            throw new ArgumentException("Idempotency key already exists");

//        var response = await next(request);
//        await _idempotentService.AddAsync(idempotencyKey, response, cancellationToken);

//        return response;
//    }
//}



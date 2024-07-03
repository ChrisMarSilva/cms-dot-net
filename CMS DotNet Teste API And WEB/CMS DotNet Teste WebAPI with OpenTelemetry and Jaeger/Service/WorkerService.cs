using Project.Database.Repositories.Interfaces;

namespace Project.Service;

public sealed class WorkerService : BackgroundService
{
    private readonly ILogger<WorkerService> _logger;
    private readonly IQueryRepository _queryRepository;
    private readonly ICommandRepository _commandRepository;

    public WorkerService(ILogger<WorkerService> logger, IQueryRepository queryRepository, ICommandRepository commandRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _queryRepository = queryRepository ?? throw new ArgumentNullException(nameof(queryRepository));
        _commandRepository = commandRepository ?? throw new ArgumentNullException(nameof(commandRepository));
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            await this.MakeCall();
            await Task.Delay(1_000 * 5, stoppingToken);
        }
    }
    private async Task MakeCall()
    {
        // Call your other service here
    }
}

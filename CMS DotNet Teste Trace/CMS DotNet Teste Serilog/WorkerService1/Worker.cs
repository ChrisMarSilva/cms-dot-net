namespace WorkerService1
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug("ExecuteAsync: {time}", DateTimeOffset.Now);

            while (!stoppingToken.IsCancellationRequested)
            {
                //_logger.LogDebug("_logger - Worker running at: {time}", DateTimeOffset.Now);
                Serilog.Log.Debug("Serilog - Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
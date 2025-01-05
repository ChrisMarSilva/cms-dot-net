using MassTransit;

namespace RabbitMQ.Worker.Worker;

public class MassTransitWorker : BackgroundService
{
    private readonly ILogger<MassTransitWorker> _logger;
    private readonly IBusControl _busControl;

    public MassTransitWorker(
        ILogger<MassTransitWorker> logger, 
        IBusControl busControl)
    {
        _logger = logger;
        _busControl = busControl;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        //while (!stoppingToken.IsCancellationRequested)
        //{
        //    if (_logger.IsEnabled(LogLevel.Information))
        //    {
        //        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
        //    }
        //    await Task.Delay(1000, stoppingToken);
        //}
        Console.WriteLine("🚀 Worker iniciado e aguardando mensagens...");
        await _busControl.StartAsync(stoppingToken);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await _busControl.StopAsync(cancellationToken);
    }
}

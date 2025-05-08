using Cache.Web.Database.Contexts;

namespace Cache.Web.Workers;

public class MessageWorker : BackgroundService
{
    private readonly ILogger<MessageWorker> _logger;
    //private readonly IBusControl _busControl;
    private readonly IServiceProvider _serviceProvider;
    private readonly IConfiguration _configuration;

    public MessageWorker(ILogger<MessageWorker> logger, IServiceProvider serviceProvider, IConfiguration configuration)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _configuration = configuration;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        //await _busControl.StartAsync(stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            await ProcessMessageAsync(stoppingToken);

            await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
            // await Task.Delay(_configuration.GetValue("Worker:Delay", 120000), stoppingToken);
        }

        await Task.CompletedTask;
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        //await _busControl.StopAsync(cancellationToken);
    }

    private async Task ProcessMessageAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("MessageWorker running at: {time}", DateTimeOffset.Now);
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            //var message = new MensagemDto(IdMsgJdPi: Guid.NewGuid().ToString(), IdMsg: $"M{DateTime.UtcNow:yyyyMMddHHmmss}00000000000MDKGPXJKU3U", TpMsg: "pain.XXX", QueueMsg: "jd.pi.bacen.send.command.queue", XmlMsg: @"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""no""?><Envelope xmlns=""https://www.bcb.gov.br/pi/pain.009/1.0""></Envelope>");
            //var queueName = _configuration["RabbitMQ:Queue"];
            //var sendProvider = scope.ServiceProvider.GetRequiredService<ISendEndpointProvider>();
            ////await sendProvider.SendAsync(queue: queueName, message: message, cancellationToken: cancellationToken); // CancellationToken.None
            //var endpoint = await sendProvider.GetSendEndpoint(new Uri($"queue:{queueName}")).ConfigureAwait(continueOnCapturedContext: false);
            //await endpoint.Send(message, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);

            // var mensagemModels = await context.Mensagens.ToListAsync(cancellationToken);
            // context.Mensagens.RemoveRange(mensagemModels);
            // await context.SaveChangesAsync(cancellationToken);

            //var mensagemModel = new MensagemModel { Id = Guid.NewGuid(), IdMsgJdPi = Guid.NewGuid().ToString(), IdMsg = $"M{DateTime.UtcNow:yyyyMMddHHmmss}00000000000MDKGPXJKU3U", TpMsg = "pain.XXX", QueueMsg = "jd.pi.bacen.send.command.queue", XmlMsg = @"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""no""?><Envelope xmlns=""https://www.bcb.gov.br/pi/pain.009/1.0""></Envelope>", DtHrRegistro = DateTime.UtcNow };
            //await context.Set<MensagemModel>().AddAsync(mensagemModel, cancellationToken);
            //await context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception)
        {
            throw;
        }
    }
}

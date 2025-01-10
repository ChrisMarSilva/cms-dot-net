using Cache.Web.Contracts;
using Cache.Web.Database.Contexts;
using Cache.Web.Models;
using MassTransit;

namespace Cache.Web.Consumers;

public class MessagemConsumer : IConsumer<MensagemDto>
{
    private readonly ILogger<MessagemConsumer> _logger;
    private readonly AppDbContext _context;

    public MessagemConsumer(ILogger<MessagemConsumer> logger, AppDbContext dbContext)
    {
        _logger = logger;
        _context = dbContext;
    }

    public async Task Consume(ConsumeContext<MensagemDto> ctx)
    {
        // _logger.LogInformation($"Mensagem recebida: {ctx.Message.XmlMsg}");
        try
        {
            var message = ctx.Message;
            var mensagemModel = new MensagemModel{ Id = Guid.NewGuid(), IdMsgJdPi = message.IdMsgJdPi!, IdMsg = message.IdMsg!, TpMsg = message.TpMsg!, QueueMsg = message.QueueMsg!, XmlMsg = message.XmlMsg!, DtHrRegistro = DateTime.UtcNow};
            _context.Mensagens.Add(mensagemModel);

            await using var transaction = await _context.Database.BeginTransactionAsync();
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erro: {ex.Message}");
            throw; // Garante que a mensagem será reencaminhada para retry
        }
    }
}

using MassTransit;
using RabbitMQ.Contratos.Requests;
using RabbitMQ.Models.Entities;
using RabbitMQ.Repositories.Database;

namespace RabbitMQ.Worker.Consumers;

public class MessageConsumer : IConsumer<MessageDto>
{
    private readonly AppDbContext _dbContext;

    public MessageConsumer(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Consume(ConsumeContext<MessageDto> context)
    {
        try
        {
            var message = new Message(context.Message.Text);
            _dbContext.Messages.Add(message);

            await using var transaction = await _dbContext.Database.BeginTransactionAsync();
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();

            //Console.WriteLine($"Mensagem salva no banco: {message.Texto}");
            // return Task.CompletedTask;
        }
        catch (Exception ex)
        {
            // transaction.Rollback();
            Console.WriteLine($"Erro ao processar mensagem: {ex.Message}");
            throw; // Garante que a mensagem será reencaminhada para retry
        }
    }
}
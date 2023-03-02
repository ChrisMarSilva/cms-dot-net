using GeekShopping.Email.Messages;
using GeekShopping.Email.Repository;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace GeekShopping.Email.MessageConsumer
{
    public class RabbitMQPaymentConsumer : BackgroundService
    {
        private readonly EmailRepository _repository;
        private IConnection _connection;
        private IModel _channel;
        //private const string ExchangeName = "FanoutPaymentUpdateExchange";
        //private string _queueName = "";
        private const string ExchangeName = "DirectPaymentUpdateExchange";
        private const string PaymentEmailUpdateQueueName = "PaymentEmailUpdateQueueName";

        public RabbitMQPaymentConsumer(EmailRepository repository)
        {
            _repository = repository;
            var factory = new ConnectionFactory{ HostName = "localhost", UserName = "guest", Password = "guest" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            // topic
            //_channel.QueueDeclare(queue: "xxxxxx", durable: false, exclusive: false, autoDelete: false, arguments: null);

            // Fanout
            //_channel.ExchangeDeclare(exchange: ExchangeName, type: ExchangeType.Fanout);
            //_queueName = _channel.QueueDeclare().QueueName;
            //_channel.QueueBind(queue: _queueName, exchange: ExchangeName, routingKey: "");

            // Direct
            _channel.ExchangeDeclare(exchange: ExchangeName, type: ExchangeType.Direct);
            _channel.QueueDeclare(queue: PaymentEmailUpdateQueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueBind(PaymentEmailUpdateQueueName, ExchangeName, "PaymentEmail");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            
            var consumer = new EventingBasicConsumer(_channel);            
            consumer.Received += (chanel, evt) =>
            {
                var content = Encoding.UTF8.GetString(evt.Body.ToArray());
                UpdatePaymentResultMessage message = JsonSerializer.Deserialize<UpdatePaymentResultMessage>(content);
                
                this.ProcessLogs(message).GetAwaiter().GetResult();
                
                _channel.BasicAck(deliveryTag: evt.DeliveryTag, multiple: false);
            };
            //_channel.BasicConsume(queue: _queueName, autoAck: false, consumer: consumer);
            _channel.BasicConsume(queue: PaymentEmailUpdateQueueName, autoAck: false, consumer: consumer);

            return Task.CompletedTask;
        }

        private async Task ProcessLogs(UpdatePaymentResultMessage message)
        {
            try
            {
                await _repository.LogEmail(message);
            }
            catch (Exception)
            {
                //Log
                throw;
            }
        }
    }
}

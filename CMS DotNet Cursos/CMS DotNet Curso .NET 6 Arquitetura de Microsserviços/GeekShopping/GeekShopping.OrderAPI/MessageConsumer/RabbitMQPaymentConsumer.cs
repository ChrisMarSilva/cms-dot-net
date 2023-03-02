using GeekShopping.OrderAPI.Messages;
using GeekShopping.OrderAPI.Repository;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace GeekShopping.OrderAPI.MessageConsumer
{
    public class RabbitMQPaymentConsumer : BackgroundService
    {
        private readonly ILogger<RabbitMQPaymentConsumer> _logger;
        private readonly OrderRepository _repository;
        private IConnection _connection;
        private IModel _channel;
        //private string _queueName = "";
        //private const string ExchangeName = "FanoutPaymentUpdateExchange";
        private const string ExchangeName = "DirectPaymentUpdateExchange";
        private const string PaymentOrderUpdateQueueName = "PaymentOrderUpdateQueueName";

        public RabbitMQPaymentConsumer(
            ILogger<RabbitMQPaymentConsumer> logger,
            OrderRepository repository
            )
        {
            _logger = logger;
            _repository = repository;

            var factory = new ConnectionFactory { HostName = "localhost", UserName = "guest", Password = "guest" };
            _connection = factory.CreateConnection();

            _channel = _connection.CreateModel();

            // topic
            //_channel.QueueDeclare(queue: "orderpaymentresultqueue", durable: false, exclusive: false, autoDelete: false, arguments: null);

            //Fanout
            //_channel.ExchangeDeclare(exchange: ExchangeName, type: ExchangeType.Fanout);
            //_queueName = _channel.QueueDeclare().QueueName;
            //_channel.QueueBind(queue: _queueName, exchange: ExchangeName, routingKey: "");

            //Direct
            _channel.ExchangeDeclare(exchange: ExchangeName, type: ExchangeType.Direct);
            _channel.QueueDeclare(queue: PaymentOrderUpdateQueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueBind(queue: PaymentOrderUpdateQueueName, exchange: ExchangeName, routingKey: "PaymentOrder");

            _logger.LogInformation("OrderAPI.RabbitMQPaymentConsumer");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("OrderAPI.RabbitMQPaymentConsumer.ExecuteAsync()");
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (chanel, evt) =>
            {
                var content = Encoding.UTF8.GetString(evt.Body.ToArray());
                UpdatePaymentResultVO vo = JsonSerializer.Deserialize<UpdatePaymentResultVO>(content);
                
                this.UpdatePaymentStatus(vo).GetAwaiter().GetResult();
               
                _channel.BasicAck(deliveryTag: evt.DeliveryTag, multiple: false);
            };

            //_channel.BasicConsume(queue: "orderpaymentresultqueue", autoAck: false, consumer: consumer);
            //_channel.BasicConsume(queue: _queueName, autoAck: false, consumer: consumer);
            _channel.BasicConsume(queue: PaymentOrderUpdateQueueName, autoAck: false, consumer: consumer);

            return Task.CompletedTask;
        }

        private async Task UpdatePaymentStatus(UpdatePaymentResultVO vo)
        {
            _logger.LogInformation("OrderAPI.RabbitMQPaymentConsumer.UpdatePaymentStatus()");
            try
            {
                await _repository.UpdateOrderPaymentStatus(vo.OrderId, vo.Status);
            }
            catch (Exception)
            {
                //Log
                throw;
            }
        }
    }
}

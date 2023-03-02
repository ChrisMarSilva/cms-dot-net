using GeekShopping.PaymentAPI.Messages;
using GeekShopping.PaymentAPI.RabbitMQSender;
using GeekShopping.PaymentProcessor;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace GeekShopping.PaymentAPI.MessageConsumer
{
    public class RabbitMQPaymentConsumer : BackgroundService
    {
        private readonly ILogger<RabbitMQPaymentConsumer> _logger;
        private IConnection _connection;
        private IModel _channel;
        private IRabbitMQMessageSender _rabbitMQMessageSender;
        private readonly IProcessPayment _processPayment;

        public RabbitMQPaymentConsumer(
            ILogger<RabbitMQPaymentConsumer> logger,
            IProcessPayment processPayment,
            IRabbitMQMessageSender rabbitMQMessageSender
            )
        {
            _logger = logger;
            _processPayment = processPayment;
            _rabbitMQMessageSender = rabbitMQMessageSender;

            var factory = new ConnectionFactory { HostName = "localhost", UserName = "guest", Password = "guest" };
            _connection = factory.CreateConnection();
            
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "orderpaymentprocessqueue", durable: false, exclusive: false, autoDelete: false, arguments: null);

            _logger.LogInformation("PaymentAPI.RabbitMQPaymentConsumer");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("PaymentAPI.RabbitMQPaymentConsumer.ExecuteAsync()");

            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (chanel, evt) =>
            {
                var content = Encoding.UTF8.GetString(evt.Body.ToArray());
                PaymentMessage vo = JsonSerializer.Deserialize<PaymentMessage>(content);
                
                this.ProcessPayment(vo).GetAwaiter().GetResult();
                
                _channel.BasicAck(deliveryTag: evt.DeliveryTag, multiple: false);
            };

            _channel.BasicConsume(queue: "orderpaymentprocessqueue", autoAck: false, consumer: consumer);

            return Task.CompletedTask;
        }

        private async Task ProcessPayment(PaymentMessage vo)
        {
            _logger.LogInformation("PaymentAPI.RabbitMQPaymentConsumer.ProcessPayment()");

            var statusPayment = _processPayment.PaymentProcessor(); // result

            UpdatePaymentResultMessage paymentResult = new()
            {
                Status = statusPayment,
                OrderId = vo.OrderId,
                Email = vo.Email
            };

            try
            {
                // _rabbitMQMessageSender.SendMessage(paymentResult, "orderpaymentresultqueue");
                _rabbitMQMessageSender.SendMessage(paymentResult);
            }
            catch (Exception)
            {
                //Log
                throw;
            }
        }
    }
}

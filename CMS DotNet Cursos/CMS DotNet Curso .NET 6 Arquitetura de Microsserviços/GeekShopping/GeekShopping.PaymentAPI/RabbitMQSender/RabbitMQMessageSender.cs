using GeekShopping.MessageBus;
using GeekShopping.PaymentAPI.Messages;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace GeekShopping.PaymentAPI.RabbitMQSender
{

    public class RabbitMQMessageSender : IRabbitMQMessageSender
    {
        private readonly ILogger<RabbitMQMessageSender> _logger;
        private readonly string _hostName;
        private readonly string _password;
        private readonly string _userName;
        private IConnection _connection;
        //private const string ExchangeName = "FanoutPaymentUpdateExchange";
        private const string ExchangeName = "DirectPaymentUpdate_Exchange";
        private const string PaymentEmailUpdateQueueName = "PaymentEmailUpdateQueueName";
        private const string PaymentOrderUpdateQueueName = "PaymentOrderUpdateQueueName";

        public RabbitMQMessageSender(ILogger<RabbitMQMessageSender> logger)
        {
            _logger = logger;
            _hostName = "localhost";
            _password = "guest";
            _userName = "guest";
            _logger.LogInformation("PaymentAPI.RabbitMQMessageSender");
        }

        //public void SendMessage(BaseMessage message, string queueName)
        public void SendMessage(BaseMessage message)
        {
            _logger.LogInformation("PaymentAPI.RabbitMQMessageSender.SendMessage()");

            if (this.ConnectionExists())
            {
                using var channel = _connection.CreateModel();

                // topic
                //channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                //Fanout
                //channel.ExchangeDeclare(exchange: ExchangeName, type: ExchangeType.Fanout, durable: false);

                //Direct
                channel.ExchangeDeclare(exchange: ExchangeName, type: ExchangeType.Direct, durable: false);
                channel.QueueDeclare(queue: PaymentEmailUpdateQueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
                channel.QueueDeclare(queue: PaymentOrderUpdateQueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
                
                channel.QueueBind(queue: PaymentEmailUpdateQueueName, exchange: ExchangeName, routingKey: "PaymentEmail");
                channel.QueueBind(queue: PaymentOrderUpdateQueueName, exchange: ExchangeName, routingKey: "PaymentOrder");

                byte[] body = GetMessageAsByteArray(message);

                //channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
                //channel.BasicPublish(exchange: ExchangeName, "", basicProperties: null, body: body);
                channel.BasicPublish(exchange: ExchangeName, "PaymentEmail", basicProperties: null, body: body);
                channel.BasicPublish(exchange: ExchangeName, "PaymentOrder", basicProperties: null, body: body);
            }
        }

        private byte[] GetMessageAsByteArray(BaseMessage message)
        {
            _logger.LogInformation("PaymentAPI.RabbitMQMessageSender.GetMessageAsByteArray()");

            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize<UpdatePaymentResultMessage>((UpdatePaymentResultMessage)message, options);
            
            var body = Encoding.UTF8.GetBytes(json);
            return body;
        }

        private bool ConnectionExists()
        {
            _logger.LogInformation("PaymentAPI.RabbitMQMessageSender.ConnectionExists()");

            if (_connection != null)
                return true;

            this.CreateConnection();

            return _connection != null;
        }
        private void CreateConnection()
        {
            _logger.LogInformation("PaymentAPI.RabbitMQMessageSender.CreateConnection()");
            try
            {
                var factory = new ConnectionFactory { HostName = _hostName, UserName = _userName, Password = _password };
                _connection = factory.CreateConnection();
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }
    }
}

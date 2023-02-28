using GeekShopping.MessageBus;
using GeekShopping.OrderAPI.Messages;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace GeekShopping.OrderAPI.RabbitMQSender
{
    public class RabbitMQMessageSender : IRabbitMQMessageSender
    {
        private readonly ILogger<RabbitMQMessageSender> _logger;
        private readonly string _hostName;
        private readonly string _password;
        private readonly string _userName;
        private IConnection _connection;

        public RabbitMQMessageSender(ILogger<RabbitMQMessageSender> logger)
        {
            _logger = logger;
            _hostName = "localhost";
            _password = "guest";
            _userName = "guest";
            _logger.LogInformation("OrderAPI.RabbitMQMessageSender");
        }

        public void SendMessage(BaseMessage message, string queueName)
        {
            _logger.LogInformation("OrderAPI.RabbitMQMessageSender.SendMessage()");

            if (this.ConnectionExists())
            {
                using var channel = _connection.CreateModel();
                channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                byte[] body = GetMessageAsByteArray(message);
                channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
            }
        }

        private byte[] GetMessageAsByteArray(BaseMessage message)
        {
            _logger.LogInformation("OrderAPI.RabbitMQMessageSender.GetMessageAsByteArray()");

            var options = new JsonSerializerOptions{ WriteIndented = true };
            var json = JsonSerializer.Serialize<PaymentVO>((PaymentVO)message, options);
            var body = Encoding.UTF8.GetBytes(json);
            
            return body;
        }

        private bool ConnectionExists()
        {
            _logger.LogInformation("OrderAPI.RabbitMQMessageSender.ConnectionExists()");

            if (_connection != null)
                return true;

            this.CreateConnection();

            return _connection != null;
        }
        private void CreateConnection()
        {
            _logger.LogInformation("OrderAPI.RabbitMQMessageSender.CreateConnection()");
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

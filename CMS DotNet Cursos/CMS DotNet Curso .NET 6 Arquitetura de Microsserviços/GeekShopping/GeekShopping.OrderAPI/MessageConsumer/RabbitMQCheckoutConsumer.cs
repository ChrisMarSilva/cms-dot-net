using GeekShopping.OrderAPI.Messages;
using GeekShopping.OrderAPI.Model;
using GeekShopping.OrderAPI.RabbitMQSender;
using GeekShopping.OrderAPI.Repository;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace GeekShopping.OrderAPI.MessageConsumer
{
    public class RabbitMQCheckoutConsumer : BackgroundService
    {
        private readonly ILogger<RabbitMQCheckoutConsumer> _logger;
        private readonly OrderRepository _repository;
        private IConnection _connection;
        private IModel _channel;
        private IRabbitMQMessageSender _rabbitMQMessageSender;

        public RabbitMQCheckoutConsumer(
            ILogger<RabbitMQCheckoutConsumer> logger,
            OrderRepository repository,
            IRabbitMQMessageSender rabbitMQMessageSender
            )
        {
            _logger = logger;
            _repository = repository;
            _rabbitMQMessageSender = rabbitMQMessageSender;

            var factory = new ConnectionFactory { HostName = "localhost", UserName = "guest", Password = "guest" };
            _connection = factory.CreateConnection();

            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "checkoutqueue", durable: false, exclusive: false, autoDelete: false, arguments: null);

            _logger.LogInformation("OrderAPI.RabbitMQCheckoutConsumer");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("OrderAPI.RabbitMQCheckoutConsumer.ExecuteAsync()");
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (chanel, evt) =>
            {
                var content = Encoding.UTF8.GetString(evt.Body.ToArray());
                CheckoutHeaderVO vo = JsonSerializer.Deserialize<CheckoutHeaderVO>(content);

                this.ProcessOrder(vo).GetAwaiter().GetResult();

                _channel.BasicAck(deliveryTag: evt.DeliveryTag, multiple: false);
            };

            _channel.BasicConsume(queue: "checkoutqueue", autoAck: false, consumer: consumer);

            return Task.CompletedTask;
        }

        private async Task ProcessOrder(CheckoutHeaderVO vo)
        {
            _logger.LogInformation("OrderAPI.RabbitMQCheckoutConsumer.ProcessOrder()");

            OrderHeader order = new()
            {
                UserId = vo.UserId,
                FirstName = vo.FirstName,
                LastName = vo.LastName,
                OrderDetails = new List<OrderDetail>(),
                CardNumber = vo.CardNumber,
                CouponCode = vo.CouponCode,
                CVV = vo.CVV,
                DiscountAmount = vo.DiscountAmount,
                Email = vo.Email,
                ExpiryMonthYear = vo.ExpiryMothYear,
                OrderTime = DateTime.Now,
                PurchaseAmount = vo.PurchaseAmount,
                PaymentStatus = false,
                Phone = vo.Phone,
                DateTime = vo.DateTime
            };

            if (vo.CartDetails != null && vo.CartDetails.Any())
            {
                foreach (var details in vo.CartDetails)
                {
                    OrderDetail detail = new()
                    {
                        ProductId = details.ProductId,
                        ProductName = details.Product.Name,
                        Price = details.Product.Price,
                        Count = details.Count,
                    };

                    order.CartTotalItens += details.Count;
                    order.OrderDetails.Add(detail);
                }
            }

            var addOrder = await _repository.AddOrder(order);

            if (!addOrder)
            {
                var errorMessage = "Order Not Add";
                _logger.LogError($"OrderAPI.RabbitMQCheckoutConsumer.ProcessOrder({errorMessage})");
                throw new Exception(errorMessage);
            }

            PaymentVO payment = new()
            {
                Name = order.FirstName + " " + order.LastName,
                CardNumber = order.CardNumber,
                CVV = order.CVV,
                ExpiryMonthYear = order.ExpiryMonthYear,
                OrderId = order.Id,
                PurchaseAmount = order.PurchaseAmount,
                Email = order.Email,
            };

            try
            {
                _rabbitMQMessageSender.SendMessage(payment, "orderpaymentprocessqueue");
            }
            catch (Exception ex)
            {
                _logger.LogError($"OrderAPI.RabbitMQCheckoutConsumer.ProcessOrder({ex.Message})");
                throw;
            }
        }
    }
}

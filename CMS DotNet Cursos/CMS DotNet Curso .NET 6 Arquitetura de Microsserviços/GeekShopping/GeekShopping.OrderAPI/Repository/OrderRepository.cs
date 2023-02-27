using GeekShopping.OrderAPI.Model;
using GeekShopping.OrderAPI.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.OrderAPI.Repository
{
    public class OrderRepository : IOrderRepository
    {
        //private readonly ILogger<OrderRepository> _logger;
        private readonly DbContextOptions<MySQLContext> _context;

        public OrderRepository(
            //ILogger<OrderRepository> logger,
            DbContextOptions<MySQLContext> context
            )
        {
            //_logger = logger;
            _context = context;
            //_logger.LogInformation("OrderAPI.OrderRepository");
        }

        public async Task<bool> AddOrder(OrderHeader header)
        {
            //_logger.LogInformation("OrderAPI.OrderRepository.AddOrder()");

            if (header == null) 
                return false;

            await using var _db = new MySQLContext(_context);
            await _db.Headers.AddAsync(header);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task UpdateOrderPaymentStatus(long orderHeaderId, bool status)
        {
            //_logger.LogInformation("OrderAPI.OrderRepository.UpdateOrderPaymentStatus()");

            await using var _db = new MySQLContext(_context);
            var header = await _db.Headers.FirstOrDefaultAsync(o => o.Id == orderHeaderId);

            if (header != null)
            {
                header.PaymentStatus = status;
                await _db.SaveChangesAsync();
            };
        }
    }
}

using GeekShopping.Email.Messages;
using GeekShopping.Email.Model;
using GeekShopping.Email.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.Email.Repository
{
    public class EmailRepository : IEmailRepository
    {

        //private readonly ILogger<EmailRepository> _logger;
        private readonly DbContextOptions<MySQLContext> _context;

        public EmailRepository(
            //ILogger<EmailRepository> logger,
            DbContextOptions<MySQLContext> context
            )
        {
            //_logger = logger;
            _context = context;
            //_logger.LogInformation("Email.EmailRepository");
        }

        public async Task LogEmail(UpdatePaymentResultMessage message)
        {
            //_logger.LogInformation("Email.EmailRepository.LogEmail()");

            EmailLog email = new EmailLog()
            {
                Email = message.Email,
                SentDate = DateTime.Now,
                Log = $"Order - {message.OrderId} has been created successfully!"
            };

            await using var _db = new MySQLContext(_context);            
            await _db.Emails.AddAsync(email);
            await _db.SaveChangesAsync();
        }
    }
}

using AutoMapper;
using GeekShopping.CartAPI.Data.ValueObjects;
using GeekShopping.CartAPI.Model;
using GeekShopping.CartAPI.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CartAPI.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly ILogger<CartRepository> _logger;
        private readonly MySQLContext _context;
        private IMapper _mapper;

        public CartRepository(
            ILogger<CartRepository> logger, 
            MySQLContext context, 
            IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _logger.LogInformation("CartAPI.CartRepository");
        }

        public async Task<bool> ApplyCoupon(string userId, string couponCode)
        {
            _logger.LogInformation("CartAPI.CartRepository.ApplyCoupon()");

            throw new NotImplementedException();
        }

        public async Task<bool> ClearCart(string userId)
        {
            _logger.LogInformation("CartAPI.CartRepository.ClearCart()");

            var cartHeader = await _context
                .CartHeaders
                .FirstOrDefaultAsync(c => c.UserId == userId);
          
            if (cartHeader == null)
                return false;

            _context.CartDetails.RemoveRange(
                _context.CartDetails.Where(c => c.CartHeaderId == cartHeader.Id));

            _context.CartHeaders.Remove(cartHeader);
            
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<CartVO> FindCartByUserId(string userId)
        {
            _logger.LogInformation("CartAPI.CartRepository.FindCartByUserId()");

            //Cart cart = new()
            //{
            //    CartHeader = await _context.CartHeaders.FirstOrDefaultAsync(c => c.UserId == userId),
            //};

            Cart cart = new Cart();

            cart.CartHeader = await _context
                .CartHeaders
                .FirstOrDefaultAsync(c => c.UserId == userId);

            cart.CartDetails = _context
                .CartDetails
                .Where(c => c.CartHeaderId == cart.CartHeader.Id)
                .Include(c => c.Product);

            return _mapper.Map<CartVO>(cart); // converter CartDataSet para CartVO 
        }

        public async Task<bool> RemoveCoupon(string userId)
        {
            _logger.LogInformation("CartAPI.CartRepository.RemoveCoupon()");

            throw new NotImplementedException();
        }

        public async Task<bool> RemoveFromCart(long cartDetailsId)
        {
            try
            {
                _logger.LogInformation("CartAPI.CartRepository.RemoveFromCart()");

                CartDetail cartDetail = await _context
                    .CartDetails
                    .FirstOrDefaultAsync(c => c.Id == cartDetailsId);

                int totalCartDetails = _context
                    .CartDetails
                    .Where(c => c.CartHeaderId == cartDetail.CartHeaderId)
                    .Count();

                _context.CartDetails.Remove(cartDetail);

                if (totalCartDetails == 1)
                {
                    var cartHeaderToRemove = await _context
                        .CartHeaders
                        .FirstOrDefaultAsync(c => c.Id == cartDetail.CartHeaderId);

                    _context.CartHeaders.Remove(cartHeaderToRemove);
                }

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<CartVO> SaveOrUpdateCart(CartVO vo)
        {
            _logger.LogInformation("CartAPI.CartRepository.SaveOrUpdateCart()");

            Cart cart = _mapper.Map<Cart>(vo); // converter CartVO para CartDataSet

            var productId = vo.CartDetails.FirstOrDefault().ProductId;
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);

            if (product == null)
            {
                product = cart.CartDetails.FirstOrDefault().Product;
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
            }

            // Check if CartHeader is null

            var cartHeader = await _context
                .CartHeaders
                .AsNoTracking() // Nãi gravar as mudar q fez na base, usado somente para pesquisar
                .FirstOrDefaultAsync(c => c.UserId == cart.CartHeader.UserId);

            if (cartHeader == null)
            {
                // Create CartHeader and CartDetails

                await _context.CartHeaders.AddAsync(cart.CartHeader);
                await _context.SaveChangesAsync();

                cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.Id;
                cart.CartDetails.FirstOrDefault().Product = null;
                
                await _context.CartDetails.AddAsync(cart.CartDetails.FirstOrDefault());
                await _context.SaveChangesAsync();
            }
            else
            {
                // If CartHeader is not null // Check if CartDetails has same product

                var cartDetail = await _context
                    .CartDetails
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => 
                        p.ProductId == productId && 
                        p.CartHeaderId == cartHeader.Id);

                if (cartDetail == null)
                {
                    // Create CartDetails

                    cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.Id;
                    cart.CartDetails.FirstOrDefault().Product = null;
                    
                    await _context.CartDetails.AddAsync(cart.CartDetails.FirstOrDefault());
                    await _context.SaveChangesAsync();
                }
                else
                {
                    // Update product count and CartDetails

                    cart.CartDetails.FirstOrDefault().Product = null;
                    cart.CartDetails.FirstOrDefault().Count += cartDetail.Count;
                    cart.CartDetails.FirstOrDefault().Id = cartDetail.Id;
                    cart.CartDetails.FirstOrDefault().CartHeaderId = cartDetail.CartHeaderId;
                    
                    _context.CartDetails.Update(cart.CartDetails.FirstOrDefault());
                    await _context.SaveChangesAsync();
                }
            }

            return _mapper.Map<CartVO>(cart); // converter CartDataSet para CartVO 
        }
    }
}

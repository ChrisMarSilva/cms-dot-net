using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VShop.CartApi.Context;
using VShop.CartApi.Dtos;
using VShop.CartApi.Models;
using VShop.CartApi.Repositories.Interfaces;

namespace VShop.CartApi.Repositories;

public class CartRepository : ICartRepository
{
    private readonly AppDbContext _context;
    private IMapper _mapper;

    public CartRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> CleanCartAsync(string userId)
    {
        var cartHeader = await _context
            .CartHeaders
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (cartHeader is null)
            return false;

        _context.CartItems.RemoveRange(_context.CartItems.Where(c => c.CartHeaderId == cartHeader.Id));
        _context.CartHeaders.Remove(cartHeader);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<CartDto> GetCartByUserIdAsync(string userId)
    {
        var cart = new Cart()
        {
            CartHeader = await _context
                .CartHeaders
                .FirstOrDefaultAsync(c => c.UserId == userId),
        };

        cart.CartItems = _context
            .CartItems
            .Where(c => c.CartHeaderId == cart!.CartHeader!.Id)
            .Include(c => c.Product);

        return _mapper.Map<CartDto>(cart);
    }

    public async Task<bool> DeleteItemCartAsync(int cartItemId)
    {
        try
        {
            var cartItem = await _context
                .CartItems
                .FirstOrDefaultAsync(c => c.Id == cartItemId);

            int total = _context.CartItems
                .Where(c => c.CartHeaderId == cartItem!.CartHeaderId)
                .Count();

            _context.CartItems.Remove(cartItem!);
            await _context.SaveChangesAsync();

            if (total == 1)
            {
                var cartHeader = await _context.CartHeaders
                    .FirstOrDefaultAsync(c => c.Id == cartItem!.CartHeaderId);

                _context.CartHeaders.Remove(cartHeader!);
                await _context.SaveChangesAsync();
            }

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<CartDto> UpdateCartAsync(CartDto cartDto)
    {
        var cart = _mapper.Map<Cart>(cartDto);
        await SaveProductInDataBase(cartDto, cart);  //salva o produto no banco 

        var cartHeader = await _context
            .CartHeaders
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.UserId == cart.CartHeader!.UserId);//Verifica se o CartHeader é null

        if (cartHeader is null)
            await CreateCartHeaderAndItems(cart); // criar o Header e os itens
        else
            await UpdateQuantityAndItems(cartDto, cart, cartHeader); // atualiza a quantidade e os itens

        return _mapper.Map<CartDto>(cart);
    }

    private async Task SaveProductInDataBase(CartDto cartDto, Cart cart)
    {
        var product = await _context
            .Products!
            .FirstOrDefaultAsync(p => p.Id == cartDto.CartItems.FirstOrDefault()!.ProductId);  //Verifica se o produto ja foi salvo senão salva

        if (product is null)
        {
            _context.Products!.Add(cart.CartItems.FirstOrDefault()!.Product); //salva o produto senão existe no bd
            await _context.SaveChangesAsync();
        }
    }

    private async Task CreateCartHeaderAndItems(Cart cart)
    {
        _context.CartHeaders.Add(cart.CartHeader!); //Cria o CartHeader e o CartItems
        await _context.SaveChangesAsync();

        cart.CartItems.FirstOrDefault()!.CartHeaderId = cart.CartHeader!.Id;
        cart.CartItems.FirstOrDefault()!.Product = default(Product)!; //null;
        _context.CartItems.Add(cart.CartItems.FirstOrDefault()!);
        await _context.SaveChangesAsync();
    }

    private async Task UpdateQuantityAndItems(CartDto cartDto, Cart cart, CartHeader? cartHeader)
    {
        var cartItem = await _context //Se CartHeader não é null - verifica se CartItems possui o mesmo produto
            .CartItems
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.ProductId == cartDto.CartItems.FirstOrDefault()!.ProductId
                                      && p.CartHeaderId == cartHeader!.Id);

        if (cartItem is null)
        {
            cart.CartItems.FirstOrDefault()!.CartHeaderId = cartHeader!.Id; // Cria o CartItems
            cart.CartItems.FirstOrDefault()!.Product = default(Product)!; // null;
            _context.CartItems.Add(cart.CartItems.FirstOrDefault()!);
        }
        else
        {
            cart.CartItems.FirstOrDefault()!.Product = default(Product)!; // null; //Atualiza a quantidade e o CartItems
            cart.CartItems.FirstOrDefault()!.Quantity += cartItem.Quantity;
            cart.CartItems.FirstOrDefault()!.Id = cartItem.Id;
            cart.CartItems.FirstOrDefault()!.CartHeaderId = cartItem.CartHeaderId;
            _context.CartItems.Update(cart.CartItems.FirstOrDefault()!);
        }

        await _context.SaveChangesAsync();
    }

    public async Task<bool> ApplyCouponAsync(string userId, string couponCode)
    {
        var cartHeaderApplyCoupon = await _context
            .CartHeaders
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (cartHeaderApplyCoupon is null)
            return false;

        cartHeaderApplyCoupon.CouponCode = couponCode;
        _context.CartHeaders.Update(cartHeaderApplyCoupon);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteCouponAsync(string userId)
    {
        var cartHeaderDeleteCoupon = await _context
            .CartHeaders
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (cartHeaderDeleteCoupon is null)
            return false;

        cartHeaderDeleteCoupon.CouponCode = "";
        _context.CartHeaders.Update(cartHeaderDeleteCoupon);

        await _context.SaveChangesAsync();

        return true;
    }
}
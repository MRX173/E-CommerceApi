using Domain.Abstractions;
using Domain.ShoppingSessionAggregate.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ShoppingSessionRepository : IShoppingSessionRepository
{
    private readonly ApplicationDbContext _context;

    public ShoppingSessionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CartItem?> GetCartItemById(Guid cartItemId)
    {
        return await _context.CartItems.FirstOrDefaultAsync(c => c.Id == cartItemId);
    }

    public async Task<ShoppingSession?> GetShoppingSessionById(Guid shoppingSessionId)
    {
        return await _context.ShoppingSessions
            .FirstOrDefaultAsync(x => x.Id == shoppingSessionId);
    }

    public async Task<ShoppingSession?> CalculateShoppingSessionPrice(Guid shoppingSessionId)
    {
        var shoppingSession = await GetShoppingSessionById(shoppingSessionId);
        var cartItems = await _context.ShoppingSessions
            .Include(x => x.CartItems)
            .ThenInclude(x => x.Product)
            .SelectMany(x => x.CartItems)
            .ToListAsync();
        foreach (var cartItem in cartItems)
        {
            if (shoppingSession != null)
            {
                shoppingSession.Total.Value += cartItem.Product.Price.Value * cartItem.Quantity;
                shoppingSession.Total.Currency = cartItem.Product.Price.Currency;
            }
        }

        return shoppingSession;
    }

    public Task UpdateShoppingSession(ShoppingSession session)
    {
        _context.ShoppingSessions.Update(session);
        return Task.CompletedTask;
    }

    public async Task CreateShoppingSession(ShoppingSession? session)
    {
        await _context.ShoppingSessions.AddAsync(session);
    }

    public async Task AddItemToShoppingSession(CartItem cartItem)
    {
        var shoppingSession = await _context.ShoppingSessions
            .Include(x => x.CartItems)
            .ThenInclude(x => x.Product)
            .FirstOrDefaultAsync(x => x.Id == cartItem.ShoppingSessionId);
        if (shoppingSession != null)
        {
            shoppingSession.AddCartItemToShoppingSession(cartItem);
        }

        await _context.CartItems.AddAsync(cartItem);
    }

    public async Task<List<CartItem>> GetShoppingSessionItemsByUserId(Guid? userId)
    {
        return await _context.ShoppingSessions
            .Include(x => x.CartItems)
            .ThenInclude(x => x.Product)
            .Where(x => x.CustumUser.Id == userId)
            .SelectMany(y => y.CartItems)
            .ToListAsync();
    }

    public Task UpdateQuantity(CartItem? cartItem)
    {
        _context.CartItems.Update(cartItem);
        return Task.CompletedTask;
    }

    public Task DeleteItemFromShoppingSession(CartItem cartItem)
    {
        _context.CartItems.Remove(cartItem);
        return Task.CompletedTask;
    }
}
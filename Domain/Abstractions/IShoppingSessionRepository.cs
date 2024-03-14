using Domain.ShoppingSessionAggregate.Entities;

namespace Domain.Abstractions;

public interface IShoppingSessionRepository
{
    Task<CartItem?> GetCartItemById(Guid cartItemId);
    Task<ShoppingSession?> GetShoppingSessionById(Guid shoppingSessionId);
    Task<ShoppingSession?> CalculateShoppingSessionPrice(Guid shoppingSessionId);
    Task UpdateShoppingSession(ShoppingSession session);
    Task CreateShoppingSession(ShoppingSession? session);
    Task AddItemToShoppingSession(CartItem cartItem);
    Task<List<CartItem>> GetShoppingSessionItemsByUserId(Guid? userId);
    Task UpdateQuantity(CartItem? cartItem);
    Task DeleteItemFromShoppingSession(CartItem cartItem);
    
}
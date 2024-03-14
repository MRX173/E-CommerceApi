using Domain.Common;
using Domain.CommonValueObject;
using Domain.Exceptions;
using Domain.UserAggregate;
using Domain.UserAggregate.Entities;
using Domain.Validators.ShoppingSessionValidators;
using FluentValidation.Results;

namespace Domain.ShoppingSessionAggregate.Entities;

public class ShoppingSession : AuditableEntityWithoutDeletion
{
    private readonly List<CartItem> _cartItems = new List<CartItem>();

    private ShoppingSession()
    {
    }

    public Price Total { get; private set; }
    public Guid? CustumUserId { get; private set; }
    public CustumUser CustumUser { get; private set; }

    public IEnumerable<CartItem> CartItems
    {
        get { return _cartItems; }
    }

    public static ShoppingSession? CreateShoppingSession(Guid? custumUserId)
    {
        ShoppingSessionValidator validator = new ShoppingSessionValidator();
        ShoppingSession? shoppingSessionToValidate = new ShoppingSession
        {
            Id = Guid.NewGuid(),
            CustumUserId = custumUserId,
            Created = DateTimeOffset.UtcNow,
            LastModified = DateTimeOffset.UtcNow,
            Total = Price.Create(0, "USD")
        };
        ValidationResult? validationResult = validator.Validate(shoppingSessionToValidate);
        if (validationResult.IsValid) return shoppingSessionToValidate;
        ShoppingSessionNotValidException exception =
            new ShoppingSessionNotValidException("Shopping session is not valid");
        validationResult.Errors.ForEach(error => exception.ValidationErrors.Add(error.ErrorMessage));
        throw exception;
    }

    public void RemoveCartItemFromShoppingSession(CartItem cartItem)
    {
        _cartItems.Remove(cartItem);
    }

    public void AddCartItemToShoppingSession(CartItem cartItem)
    {
        _cartItems.Add(cartItem);
    }
}
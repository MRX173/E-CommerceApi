using Domain.Common;
using Domain.Exceptions;
using Domain.ProductAggregate;
using Domain.ProductAggregate.Entities;
using Domain.Validators.ShoppingSessionValidators;
using FluentValidation.Results;

namespace Domain.ShoppingSessionAggregate.Entities;

public class CartItem : AuditableEntityWithoutDeletion
{
    private CartItem()
    {
    }

    public int Quantity { get; private set; }
    public Guid ProductId { get; private set; }
    public Product Product { get; private set; }
    public Guid ShoppingSessionId { get; private set; }
    public ShoppingSession ShoppingSession { get; private set; }

    public static CartItem CreateCartItem(int quantity, Guid shoppingSessionId, Guid ProductId)
    {
        CartItemValidator validator = new CartItemValidator();
        CartItem cartItemToValidate = new CartItem
        {
            Id = Guid.NewGuid(),
            Quantity = quantity,
            ShoppingSessionId = shoppingSessionId,
            ProductId = ProductId,
            Created = DateTimeOffset.UtcNow,
            LastModified = DateTimeOffset.UtcNow
        };
        ValidationResult? validationResult = validator.Validate(cartItemToValidate);
        if (validationResult.IsValid) return cartItemToValidate;
        CartItemNotValidException exception = new CartItemNotValidException("Cart item is not valid");
        validationResult.Errors.ForEach(error => exception.ValidationErrors.Add(error.ErrorMessage));
        throw exception;
    }

    public void UpdateQuantityCartItem(int quantity)
    {
        Quantity = quantity;
    }
}
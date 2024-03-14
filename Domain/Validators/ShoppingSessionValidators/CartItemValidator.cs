using Domain.ShoppingSessionAggregate.Entities;
using FluentValidation;

namespace Domain.Validators.ShoppingSessionValidators;

public class CartItemValidator : AbstractValidator<CartItem>
{
    public CartItemValidator()
    {
        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .NotNull()
            .WithMessage("Quantity must be greater than zero");
    }
}
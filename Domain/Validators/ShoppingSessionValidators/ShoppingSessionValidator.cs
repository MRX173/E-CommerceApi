using Domain.ShoppingSessionAggregate.Entities;
using FluentValidation;

namespace Domain.Validators.ShoppingSessionValidators;

public class ShoppingSessionValidator : AbstractValidator<ShoppingSession>
{
    public ShoppingSessionValidator()
    {
        // RuleFor(x => x.Total.Value)
        //     .GreaterThanOrEqualTo(0)
        //     .NotNull()
        //     .NotNull()
        //     .WithMessage("Total must be greater than zero or Empty");
    }
}
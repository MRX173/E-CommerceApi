using Domain.ProductAggregate.Entities;
using FluentValidation;

namespace Domain.Validators.ProductValidators;

public class ProductRateValidator : AbstractValidator<ProductRate>
{
    public ProductRateValidator()
    {
        RuleFor(x => x.RateValue)
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(5)
            .WithMessage("The rate value should be between 0 and 5");
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .NotNull();
        RuleFor(x => x.CustumUserId)
            .NotEmpty()
            .NotNull();
    }
}
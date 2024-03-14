using Domain.ProductAggregate;
using Domain.ProductAggregate.Entities;
using FluentValidation;

namespace Domain.Validators.ProductValidators;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);
        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(1000)
            .WithMessage("Product description is required");
        RuleFor(x => x.Price.Value)
            .GreaterThan(0)
            .WithMessage("Product price should be greater than 0");
        RuleFor(x => x.Price.Currency)
            .NotEmpty()
            .NotNull();
        RuleFor(x => x.Sku.Code)
            .NotEmpty()
            .WithMessage("Please enter product sku code");
    }
}
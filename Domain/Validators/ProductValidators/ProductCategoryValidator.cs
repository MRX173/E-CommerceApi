using Domain.ProductAggregate;
using FluentValidation;

namespace Domain.Validators.ProductValidators;

public class ProductCategoryValidator : AbstractValidator<ProductCategory>
{
    public ProductCategoryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100)
            .NotNull()
            .WithMessage("Product category name can't be null or empty");
        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(100)
            .NotNull()
            .WithMessage("Product category description can't be null or empty");
        RuleFor(x => x.ImageUrl)
            .NotEmpty()
            .NotNull()
            .WithMessage("Product category image url can't be null or empty");
    }
}
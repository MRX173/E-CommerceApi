using Domain.ProductAggregate.Entities;
using FluentValidation;

namespace Domain.Validators.ProductValidators;

public class ProductCommentValidator : AbstractValidator<ProductComment>
{
    public ProductCommentValidator()
    {
        RuleFor(x => x.Text)
            .NotEmpty()
            .NotNull()
            .MaximumLength(100)
            .WithMessage("Product comment text is required");
        RuleFor(x => x.ProductId)
            .NotNull()
            .NotEmpty();
        RuleFor(x => x.CustumUserId)
            .NotNull()
            .NotEmpty();
    }
}
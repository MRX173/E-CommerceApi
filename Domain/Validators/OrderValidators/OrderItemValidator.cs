using Domain.OrderAggregate.Entities;
using FluentValidation;

namespace Domain.Validators.OrderValidators;

public class OrderItemValidator : AbstractValidator<OrderItem>
{
    public OrderItemValidator()
    {
        RuleFor(x => x.Quantity)
            .NotEmpty()
            .NotNull()
            .WithMessage("Quantity can't be null or empty");
    }
}
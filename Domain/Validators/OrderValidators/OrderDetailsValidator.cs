using Domain.OrderAggregate;
using FluentValidation;

namespace Domain.Validators.OrderValidators;

public class OrderDetailsValidator : AbstractValidator<OrderDetails>
{
    public OrderDetailsValidator()
    {
        // RuleFor(x => x.TotalPrice.Value)
        //     .GreaterThanOrEqualTo(0)
        //     .WithMessage("Total price should be greater than or equal 0");
    }
}
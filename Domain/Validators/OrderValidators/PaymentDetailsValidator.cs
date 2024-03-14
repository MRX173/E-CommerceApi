using Domain.OrderAggregate.Entities;
using FluentValidation;

namespace Domain.Validators.OrderValidators;

public class PaymentDetailsValidator : AbstractValidator<PaymentDetails>
{
    public PaymentDetailsValidator()
    {
        RuleFor(x => x.Amount.Value)
            .GreaterThan(0)
            .NotEmpty()
            .NotNull()
            .WithMessage("Amount must be greater than 0 and not null");
        RuleFor(x => x.Amount.Currency)
            .NotEmpty()
            .NotNull()
            .WithMessage("Currency must not be null or empty");
        RuleFor(x => x.Provider)
            .NotEmpty()
            .NotNull()
            .WithMessage("Provider must not be null or empty");
        RuleFor(x => x.PaymentStatus)
            .NotEmpty()
            .NotNull()
            .WithMessage("Payment status must not be null or empty");
    }
}
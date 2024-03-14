using Domain.UserAggregate.Entities;
using FluentValidation;

namespace Domain.Validators.UserValidators;

public sealed class UserPaymentValidator : AbstractValidator<UserPayment>
{
    public UserPaymentValidator()
    {
        // RuleFor(x => x.PaymentType)
        //     .NotEmpty()
        //     .NotNull()
        //     .IsInEnum()
        //     .WithMessage("Please enter a valid payment type ");
        // RuleFor(x => x.Provider)
        //     .NotEmpty()
        //     .NotNull()
        //     .IsInEnum()
        //     .WithMessage("Please enter a valid provider ");
        RuleFor(x => x.CardNumber)
            .NotEmpty()
            .NotNull()
            .CreditCard()
            .WithMessage("Please enter a valid card number ");
        RuleFor(x => x.ExpirationDate)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid expiration date ");
    }
}
using Domain.UserAggregate.Entities;
using FluentValidation;

namespace Domain.Validators.UserValidators;

public sealed class CustumUserValidator : AbstractValidator<CustumUser>
{
    public CustumUserValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .NotNull()
            .MaximumLength(10)
            .WithMessage("Please enter a valid first name");
        
        RuleFor(x => x.LastName)
            .NotEmpty()
            .NotNull()
            .MaximumLength(10)
            .WithMessage("Please enter a valid last name");
        
        RuleFor(x => x.UserAddress.AddressLine)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid address");
        RuleFor(x => x.UserAddress.City)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid city");
        RuleFor(x => x.UserAddress.Country)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid country");

        RuleFor(x => x.Email)
            .EmailAddress()
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid email");
        
        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid phone number");
    }
}
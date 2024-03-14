using Domain.UserAggregate.Entities;
using FluentValidation;

namespace Domain.Validators.UserValidators;

public sealed class UserRoleValidator : AbstractValidator<UserRole>
{
    public UserRoleValidator()
    {
        RuleFor(userRole => userRole.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Role name can't be null or empty");
    }
}
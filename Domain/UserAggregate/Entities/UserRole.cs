using Domain.Exceptions.UserExceptions;
using Domain.Validators.UserValidators;
using Microsoft.AspNetCore.Identity;

namespace Domain.UserAggregate.Entities;

public class UserRole : IdentityRole<Guid>
{
    private readonly List<Permission> _permissions = new List<Permission>();

    private UserRole()
    {
    }

    public IEnumerable<Permission> Permissions => _permissions;

    public static UserRole Create(string roleName)
    {
        var validator = new UserRoleValidator();
        var userRoleToValidate = new UserRole
        {
            Id = Guid.NewGuid(),
            Name = roleName
        };
        var validationResult = validator.Validate(userRoleToValidate);
        if (validationResult.IsValid) return userRoleToValidate;
        UserRoleNotValidException exception = new UserRoleNotValidException("User role is not valid");
        validationResult.Errors.ForEach(error => exception.ValidationErrors.Add(error.ErrorMessage));
        throw exception;
    }

    public UserRole UpdateUserRole(string roleName)
    {
        if (string.IsNullOrEmpty(roleName))
        {
            var exception = new UserRoleNotValidException("Role name is not valid");
            exception.ValidationErrors.Add(" role name is not valid");
            throw exception;
        }

        Name = roleName;
        return this;
    }
}
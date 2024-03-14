using Domain.Exceptions.BaseException;

namespace Domain.Exceptions.UserExceptions;

public sealed class UserRoleNotValidException : DomainException
{
    internal UserRoleNotValidException() { }
    internal UserRoleNotValidException(string message) : base(message) { }
    internal UserRoleNotValidException(string message, Exception inner) : base(message, inner) { }
}
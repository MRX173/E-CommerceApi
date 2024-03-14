using Domain.Exceptions.BaseException;

namespace Domain.Exceptions.UserExceptions;

public sealed class UserNotValidException : DomainException
{
    
    internal UserNotValidException() { }
    internal UserNotValidException(string message) : base(message) { }
    internal UserNotValidException(string message, Exception inner) : base(message, inner) { }
}
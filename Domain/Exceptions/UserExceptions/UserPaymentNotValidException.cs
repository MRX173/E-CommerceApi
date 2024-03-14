using Domain.Exceptions.BaseException;

namespace Domain.Exceptions.UserExceptions;

public sealed class UserPaymentNotValidException : DomainException
{
    
    internal UserPaymentNotValidException() { }
    internal UserPaymentNotValidException(string message) : base(message) { }
    internal UserPaymentNotValidException(string message, Exception inner) : base(message, inner) { }
}
using Domain.Exceptions.BaseException;

namespace Domain.Exceptions.OrderExceptions;

public sealed class OrderDetailsNotValidException : DomainException
{
    internal OrderDetailsNotValidException() { }
    internal OrderDetailsNotValidException(string message) : base(message) { }
    internal OrderDetailsNotValidException(string message, Exception inner) : base(message, inner) { }
}
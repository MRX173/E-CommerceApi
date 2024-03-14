using Domain.Exceptions.BaseException;

namespace Domain.Exceptions.OrderExceptions;

public sealed class OrderItemNotValidException : DomainException
{
    internal OrderItemNotValidException() { }
    internal OrderItemNotValidException(string message) : base(message) { }
    internal OrderItemNotValidException(string message, Exception inner) : base(message, inner) { }
}
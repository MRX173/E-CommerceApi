using Domain.Exceptions.BaseException;

namespace Domain.Exceptions;

public class ShoppingSessionNotValidException : DomainException
{
    internal ShoppingSessionNotValidException() { }
    internal ShoppingSessionNotValidException(string message) : base(message) { }
    internal ShoppingSessionNotValidException(string message, Exception inner) : base(message, inner) { }
}
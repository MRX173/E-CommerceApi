using Domain.Exceptions.BaseException;

namespace Domain.Exceptions;

public  sealed class CartItemNotValidException : DomainException
{
    internal CartItemNotValidException() { }
    internal CartItemNotValidException(string message) : base(message) { }
    internal CartItemNotValidException(string message, Exception inner) : base(message, inner) { }
}
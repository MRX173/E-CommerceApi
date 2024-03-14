using Domain.Exceptions.BaseException;

namespace Domain.Exceptions.ProductExceptions;

public sealed class ProductNotValidException : DomainException
{
    internal ProductNotValidException() { }
    internal ProductNotValidException(string message) : base(message) { }
    internal ProductNotValidException(string message, Exception inner) : base(message, inner) { }
    
}
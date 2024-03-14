using Domain.Exceptions.BaseException;

namespace Domain.Exceptions.ProductExceptions;

public sealed class ProductRateNotValidException : DomainException
{
    internal ProductRateNotValidException() { }
    internal ProductRateNotValidException(string message) : base(message) { }
    internal ProductRateNotValidException(string message, Exception inner) : base(message, inner) { }
}
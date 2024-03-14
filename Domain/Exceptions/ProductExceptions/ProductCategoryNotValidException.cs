using Domain.Exceptions.BaseException;

namespace Domain.Exceptions.ProductExceptions;

public sealed class ProductCategoryNotValidException : DomainException
{
    internal ProductCategoryNotValidException() { }
    internal ProductCategoryNotValidException(string message) : base(message) { }
    internal ProductCategoryNotValidException(string message, Exception inner) : base(message, inner) { }
}
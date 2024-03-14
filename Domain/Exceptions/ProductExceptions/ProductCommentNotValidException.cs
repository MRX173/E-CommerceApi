using Domain.Exceptions.BaseException;

namespace Domain.Exceptions.ProductExceptions;

public sealed class ProductCommentNotValidException : DomainException
{
    internal ProductCommentNotValidException() { }
    internal ProductCommentNotValidException(string message) : base(message) { }
    internal ProductCommentNotValidException(string message, Exception inner) : base(message, inner) { }
}
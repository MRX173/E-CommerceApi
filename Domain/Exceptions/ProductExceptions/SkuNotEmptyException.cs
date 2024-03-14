using Domain.Exceptions.BaseException;

namespace Domain.Exceptions.ProductExceptions;

public sealed class SkuNotEmptyException : DomainException
{
    internal SkuNotEmptyException(string message) : base(message)
    {
    }
}
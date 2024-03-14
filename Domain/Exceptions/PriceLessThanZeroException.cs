using Domain.Exceptions.BaseException;

namespace Domain.Exceptions;

public sealed class PriceLessThanZeroException : DomainException
{
    internal PriceLessThanZeroException(string message) : base(message){ }
}
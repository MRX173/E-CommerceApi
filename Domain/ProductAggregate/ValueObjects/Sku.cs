using Domain.Common;
using Domain.Exceptions;
using Domain.Exceptions.ProductExceptions;

namespace Domain.CommonValueObject;

public sealed class Sku : ValueObject
{
    private Sku() { }
    public string Code { get; private set; }
    public static Sku Create(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new SkuNotEmptyException("SKU code must not be empty.");
        return new Sku
        {
            Code = code
        };
    }
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Code;
    }
}
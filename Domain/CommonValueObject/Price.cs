using Domain.Common;
using Domain.Exceptions;

namespace Domain.CommonValueObject;

public sealed class Price : ValueObject
{
    private Price()
    {
    }

    public decimal Value { get; set; }
    public string Currency { get; set; }
    public static Price Create(decimal value, string currency)
    {
        if (value < 0)
            throw new PriceLessThanZeroException("Amount must be non-negative.");
        return new Price
        {
            Value = value,
            Currency = currency
        };
    }

    public void UpdatePrice(decimal value, string currency)
    {
        if (value < 0)
            throw new PriceLessThanZeroException("Amount must be non-negative.");
        Value = value;
        Currency = currency;
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
        yield return Currency;
    }
}
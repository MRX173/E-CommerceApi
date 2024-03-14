using Domain.Common;
using Domain.CommonValueObject;
using Domain.Enums;

namespace Domain.ProductAggregate;

public class Discount : ValueObject
{
    #region PrivateConstructor

    private Discount()
    {
    }

    #endregion
    
    public decimal Percentage { get; private set; }
    public IsActive IsActive { get; private set; }
    
    
    
    public static Discount CreateDiscount(decimal percentage, bool activeOrNot)
    {
        return new Discount
        {
            Percentage = percentage,
            IsActive = IsActive.IsActiveStatus(activeOrNot),
        };
    }
    public void UpdateActivationOfDiscount(bool activeOrNot)
    {
        IsActive = IsActive.IsActiveStatus(activeOrNot);
    }
    public void UpdatePercentageOfDiscount(decimal percentage)
    {
        Percentage = percentage;
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Percentage;
        yield return IsActive;
    }
}
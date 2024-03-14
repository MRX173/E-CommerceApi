using Domain.Common;
using Domain.Enums;

namespace Domain.CommonValueObject;

public class IsActive : ValueObject
{
    private IsActive(){}
    public bool Active { get; private set; }
    

    public static IsActive IsActiveStatus(bool isActive)
    {
        return new IsActive { Active = isActive
                ? Convert.ToBoolean(ActiveOrNot.Active)
                : Convert.ToBoolean(ActiveOrNot.NotActive)
        };
    }
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Active;
    }
}
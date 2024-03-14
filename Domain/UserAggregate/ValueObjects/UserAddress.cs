using Domain.Common;

namespace Domain.UserAggregate.ValueObjects;

public class UserAddress : ValueObject
{
    private UserAddress(){}
    public string AddressLine { get; private set; } = string.Empty;
    public string City { get; private set; } = string.Empty;
    public string Country { get; private set; } = string.Empty;


    public static UserAddress CreateUserAddress(string addressLine, string city, string country)
    {
        return new UserAddress
        {
            AddressLine = addressLine,
            City = city,
            Country = country
        };
    }

    public UserAddress? UpdateUserAddress(string addressLine, string city, string country)
    {
        AddressLine = addressLine;
        City = city;
        Country = country;
        return this;
    }
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return AddressLine;
        yield return City;
        yield return Country;
    }
}
namespace Domain.UserAggregate.Entities;

public class Permission
{
    private Permission()
    {
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public static Permission Create(string name)
    {
        return new Permission
        {
            Id = Guid.NewGuid(),
            Name = name
        };
    }
}
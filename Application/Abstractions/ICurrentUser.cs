namespace Application.Abstractions;

public interface ICurrentUser
{
    Guid? UserId { get; }
}
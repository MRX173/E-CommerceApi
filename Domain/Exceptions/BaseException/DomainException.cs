namespace Domain.Exceptions.BaseException;

public class DomainException : Exception
{
    // TODO : sealed classes
    internal DomainException()
    {
        ValidationErrors = new List<string>();
    }

    internal DomainException(string message) : base(message)
    {
        ValidationErrors = new List<string>();
    }

    internal DomainException(string message, Exception inner) : base(message, inner)
    {
        ValidationErrors = new List<string>();
    }
    public List<string> ValidationErrors { get;  }
}
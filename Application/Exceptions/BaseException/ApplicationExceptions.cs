namespace Application.Exceptions.BaseException;

public class ApplicationExceptions : Exception
{
    internal ApplicationExceptions()
    {
        ValidationErrors = new List<string>();
    }

    internal ApplicationExceptions(string message) : base(message)
    {
        ValidationErrors = new List<string>();
    }

    internal ApplicationExceptions(string message, Exception inner) : base(message, inner)
    {
        ValidationErrors = new List<string>();
    }
    public List<string> ValidationErrors { get;  }
}
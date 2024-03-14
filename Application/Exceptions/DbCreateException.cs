using Application.Exceptions.BaseException;

namespace Application.Exceptions;

public class DbCreateException : ApplicationExceptions
{
    public DbCreateException(){}
    public DbCreateException(string message) : base(message){}

    public DbCreateException(string message, Exception inner) : base(message, inner){}
}
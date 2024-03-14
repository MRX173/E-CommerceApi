using Application.Exceptions.BaseException;

namespace Application.Exceptions;

public class DbDeleteException : ApplicationExceptions
{
    public DbDeleteException(){}
    public DbDeleteException(string message) : base(message){}

    public DbDeleteException(string message, Exception inner) : base(message, inner){}
}
using Application.Exceptions.BaseException;

namespace Application.Exceptions;

public class DbUpdateException : ApplicationExceptions
{
    public DbUpdateException(){}
    public DbUpdateException(string message) : base(message){}

    public DbUpdateException(string message, Exception inner) : base(message, inner){}
    
}
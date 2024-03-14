using System.Reflection.Metadata;
using Application.Enums;

namespace Application.Models;

public class OperationResult<T>
{
    public T Payload { get; set; }
    public bool IsError { get; private set; }
    public List<Error> Errors { get; } = new List<Error>();

    public void AddError(ErrorCode errorCode, string message)
    {
        HandleError(errorCode, message);
    }
    public void ResetIsErrorFlag()
    {
        IsError = false;
    }
    private void HandleError(ErrorCode errorCode, string message)
    {
        Errors.Add(new Error{Code = errorCode, Message = message});
        IsError = true;
    }
}
using Application.Enums;

namespace Application.Models;

public class Error
{
    public ErrorCode Code { get; set; }
    public string Message { get; set; } = string.Empty;
}
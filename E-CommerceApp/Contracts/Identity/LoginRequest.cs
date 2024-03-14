namespace E_CommerceApp.Contracts.Identity;

public class LoginRequest
{
    public required string EmailAddress { get; set; }
    public required string Password { get; set; }
}
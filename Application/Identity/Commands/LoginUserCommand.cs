using Application.Identity.DTOs;
using Application.Models;
using MediatR;

namespace Application.Identity.Commands;

public class LoginUserCommand : IRequest<OperationResult<IdentityUserDto>>
{
    public required string EmailAddress { get; set; }
    public required string Password { get; set; }
}
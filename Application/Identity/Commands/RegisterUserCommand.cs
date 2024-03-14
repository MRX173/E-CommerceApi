using Application.Identity.DTOs;
using Application.Models;
using Domain.UserAggregate.Entities;
using MediatR;

namespace Application.Identity.Commands;

public class RegisterUserCommand : IRequest<OperationResult<IdentityUserDto>>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string EmailAddress { get; set; }
    public required string Password { get; set; }
    public required string PhoneNumber { get; set; }
    public required string AddressLine { get; set; }
    public required string City { get; set; }
    public required string Country { get; set; }
}
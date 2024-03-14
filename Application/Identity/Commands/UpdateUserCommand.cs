using Application.Identity.DTOs;
using Application.Models;
using MediatR;

namespace Application.Identity.Commands;

public class UpdateUserCommand : IRequest<OperationResult<IdentityUserDto>>
{
    public required Guid UserId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string PhoneNumber { get; set; }
    public required string AddressLine { get; set; }
    public required string City { get; set; }
    public required string Country { get; set; }
}
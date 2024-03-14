using Application.Models;
using Domain.UserAggregate.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Roles.Commands;

public class CreateRoleCommand : IRequest<OperationResult<UserRole>>
{
    public required string Name { get; set; }
}
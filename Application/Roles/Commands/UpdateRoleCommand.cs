using Application.Models;
using Domain.UserAggregate.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Roles.Commands;

public class UpdateRoleCommand : IRequest<OperationResult<UserRole>>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
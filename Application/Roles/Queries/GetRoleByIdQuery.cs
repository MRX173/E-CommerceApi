using Application.Models;
using Domain.UserAggregate.Entities;
using MediatR;

namespace Application.Roles.Queries;

public class GetRoleByIdQuery : IRequest<OperationResult<UserRole>>
{
    public Guid Id { get; set; }
}
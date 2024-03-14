using Application.Models;
using Domain.UserAggregate.Entities;
using MediatR;

namespace Application.Roles.Queries;

public class GetRolesQuery : IRequest<OperationResult<List<UserRole>>>
{
    
}
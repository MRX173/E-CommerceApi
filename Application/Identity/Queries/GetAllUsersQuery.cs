using Application.Models;
using Domain.UserAggregate.Entities;
using MediatR;

namespace Application.Identity.Queries;

public class GetAllUsersQuery : IRequest<OperationResult<List<CustumUser>>>
{
    
}
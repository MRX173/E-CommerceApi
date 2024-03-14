using Application.Models;
using Domain.UserAggregate.Entities;
using MediatR;

namespace Application.Identity.Queries;

public class GetUserByIdQuery : IRequest<OperationResult<CustumUser>>
{
    public required Guid UserId { get; set; }
}
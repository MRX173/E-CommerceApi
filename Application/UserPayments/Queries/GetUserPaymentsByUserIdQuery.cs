using Application.Models;
using Domain.UserAggregate.Entities;
using MediatR;

namespace Application.UserPayments.Queries;

public class GetUserPaymentsByUserIdQuery : IRequest<OperationResult<List<UserPayment?>>>
{
    public Guid UserId { get; set; }
}
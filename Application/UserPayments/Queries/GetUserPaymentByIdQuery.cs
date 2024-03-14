using Application.Models;
using Domain.UserAggregate.Entities;
using MediatR;

namespace Application.UserPayments.Queries;

public class GetUserPaymentByIdQuery : IRequest<OperationResult<UserPayment>>
{
    public Guid UserPaymentId { get; set; }
}
using Application.Models;
using Domain.Enums;
using Domain.UserAggregate.Entities;
using MediatR;

namespace Application.UserPayments.Commands;

public class CreateUserPaymentCommand : IRequest<OperationResult<UserPayment>>
{
    public required Guid UserId { get; set; }
    public required string CardNumber { get; set; }
    public required Provider Provider { get; set; }
    public required PaymentType PaymentType { get; set; }
    public required DateTimeOffset ExpirationDate { get; set; }
}
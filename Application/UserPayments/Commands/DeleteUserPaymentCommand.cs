using Application.Models;
using MediatR;

namespace Application.UserPayments.Commands;

public class DeleteUserPaymentCommand : IRequest<OperationResult<bool>>
{
    public Guid UserPaymentId { get; set; }
}
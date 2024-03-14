using Application.Models;
using Domain.OrderAggregate;
using MediatR;

namespace Application.Orders.Commands;

public class DeleteOrderItemCommand : IRequest<OperationResult<bool>>
{
    public required Guid OrderId { get; set; }
}
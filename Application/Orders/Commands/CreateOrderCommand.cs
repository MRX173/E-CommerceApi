using Application.Models;
using Domain.OrderAggregate;
using MediatR;

namespace Application.Orders.Commands;

public class CreateOrderCommand : IRequest<OperationResult<OrderDetails>>
{
    public required Guid UserId { get; set; }
}
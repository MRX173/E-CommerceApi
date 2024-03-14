using Application.Models;
using Domain.OrderAggregate.Entities;
using MediatR;

namespace Application.Orders.Commands;

public class UpdateQuantityOrderItemCommand : IRequest<OperationResult<OrderItem>>
{
    public required Guid OrderItemId { get; set; } 
    public required int Quantity { get; set; }
}
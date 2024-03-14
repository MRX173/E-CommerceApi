using Application.Models;
using Domain.OrderAggregate.Entities;
using MediatR;

namespace Application.Orders.Commands;

public class AddOrderItemCommand : IRequest<OperationResult<OrderItem>>
{
    public required int Quantity { get; set; }
    public required Guid OrderDetailsId { get; set; }
    public required Guid ProductId { get; set; }
}
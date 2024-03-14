using Application.Models;
using Domain.OrderAggregate;
using MediatR;

namespace Application.Orders.Queries;

public class GetAllOrdersByUserIdQuery : IRequest<OperationResult<List<OrderDetails>>>
{
    public required Guid UserId { get; set; }
}
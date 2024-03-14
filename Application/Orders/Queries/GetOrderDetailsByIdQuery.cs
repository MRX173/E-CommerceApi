using Application.Models;
using Domain.OrderAggregate;
using MediatR;

namespace Application.Orders.Queries;

public class GetOrderDetailsByIdQuery : IRequest<OperationResult<OrderDetails>>
{
    public required Guid OrderId { get; set; }
}
using Application.Orders.Commands;
using Application.Orders.Queries;
using Domain.OrderAggregate;
using E_CommerceApp.Contracts.Order.Request;
using E_CommerceApp.Contracts.Order.Response;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceApp.Controllers;

public class OrdersController : BaseController
{
    [HttpGet]
    [Route(ApiRoutes.Order.GetOrdersByUserId)]
    public async Task<IActionResult> GetAllOrdersByUserId(Guid userId, CancellationToken cancellationToken)
    {
        var query = new GetAllOrdersByUserIdQuery { UserId = userId };
        var result = await _mediator.Send(query, cancellationToken);
        var mapped = _mapper.Map<List<OrderResponse>>(result.Payload);
        return result.IsError ? HandleErrorResponses(result.Errors) : Ok(mapped);
    }

    [HttpPost]
    [Route(ApiRoutes.Order.Create)]
    public async Task<IActionResult> CreateOrder(Guid userId,
        CancellationToken cancellationToken)
    {
        // TODO: Add Current User Id automatically
        var order = new CreateOrderCommand { UserId = userId };
        var result = await _mediator.Send(order, cancellationToken);
        var mapped = _mapper.Map<OrderResponse>(result.Payload);
        return result.IsError ? HandleErrorResponses(result.Errors) : Ok(mapped);
    }

    [HttpPost]
    [Route(ApiRoutes.Order.AddOrderItem)]
    public async Task<IActionResult> AddOrderItem([FromBody] OrderItemRequest orderItemRequest,
        CancellationToken cancellationToken)
    {
        var item = _mapper.Map<AddOrderItemCommand>(orderItemRequest);
        var result = await _mediator.Send(item, cancellationToken);
        var mapped = _mapper.Map<OrderItemResponse>(result.Payload);
        return result.IsError ? HandleErrorResponses(result.Errors) : Ok(mapped);
    }

    [HttpPatch]
    [Route(ApiRoutes.Order.UpdateQuantity)]
    public async Task<IActionResult> UpdateQuantity(Guid orderItemId, [FromBody] int quantity,
        CancellationToken cancellationToken)
    {
        var item = new UpdateQuantityOrderItemCommand
        {
            Quantity = quantity, OrderItemId = orderItemId
        };
        var result = await _mediator.Send(item, cancellationToken);
        var mapped = _mapper.Map<OrderItemResponse>(result.Payload);
        return result.IsError ? HandleErrorResponses(result.Errors) : Ok(mapped);
    }
}
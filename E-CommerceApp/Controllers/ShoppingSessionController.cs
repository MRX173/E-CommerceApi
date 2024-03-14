using Application.Abstractions;
using Application.ShoppingSession.Commands;
using Application.ShoppingSession.Queries;
using Application.ShoppingSessions.Commands;
using Application.ShoppingSessions.Queries;
using Domain.UserAggregate.Entities;
using E_CommerceApp.Contracts.ShoppingSessions.Request;
using E_CommerceApp.Contracts.ShoppingSessions.Response;
using E_CommerceApp.Services;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceApp.Controllers;

// [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ShoppingSessionController : BaseController
{
    //private readonly ICurrentUser _currentUser;

    // public ShoppingSessionController(ICurrentUser currentUser)
    // {
    //     _currentUser = currentUser;
    // }

    [HttpGet]
    [Route(ApiRoutes.ShoppingSession.GetCartItem)]
    public async Task<IActionResult> GetCartItemById([FromRoute] Guid cartItemId
        , CancellationToken cancellationToken)
    {
        var query = new GetCartItemByIdQuery { CartItemId = cartItemId };
        var result = await _mediator.Send(query, cancellationToken);
        var mapped = _mapper.Map<CartItemResponse>(result.Payload);
        return result.IsError ? HandleErrorResponses(result.Errors) : Ok(mapped);
    }

    [HttpGet]
    [Route(ApiRoutes.ShoppingSession.GetShoppingSessionItems)]
    public async Task<IActionResult> GetShoppingSessionItemsByUserId(Guid userId
        , CancellationToken cancellationToken)
    {
        var query = new GetShoppingSessionItemsByUserIdQuery { UserId = userId };
        var result = await _mediator.Send(query, cancellationToken);
        var mapped = _mapper.Map<List<CartItemResponse>>(result.Payload);
        return result.IsError ? HandleErrorResponses(result.Errors) : Ok(mapped);
    }

    [HttpPost]
    [Route(ApiRoutes.ShoppingSession.Create)]
    public async Task<IActionResult> CreateShoppingSession(Guid userId
        , CancellationToken cancellationToken)
    {
        var command = new CreateShoppingSessionCommand { UserId = userId };
        var result = await _mediator.Send(command, cancellationToken);
        var mapped = _mapper.Map<ShoppingSessionResponse>(result.Payload);
        return result.IsError ? HandleErrorResponses(result.Errors) : Ok(mapped);
    }

    [HttpPost]
    [Route(ApiRoutes.ShoppingSession.AddItem)]
    public async Task<IActionResult> AddItemToShoppingSession([FromBody] AddItemRequest request
        , CancellationToken cancellationToken)
    {
        var command = _mapper.Map<AddItemToShoppingSessionCommand>(request);
        var result = await _mediator.Send(command, cancellationToken);
        var mapped = _mapper.Map<CartItemResponse>(result.Payload);
        return result.IsError ? HandleErrorResponses(result.Errors) : Ok(mapped);
    }

    [HttpPatch]
    [Route(ApiRoutes.ShoppingSession.CalculateShoppingSessionPrice)]
    public async Task<IActionResult> CalculateShoppingSessionPrice(Guid shoppingSessionId,
        CancellationToken cancellationToken)
    {
        var command = new CalculateShoppingSessionPriceCommand { Id = shoppingSessionId };
        var result = await _mediator.Send(command, cancellationToken);
        var mapped = _mapper.Map<ShoppingSessionResponse>(result.Payload);
        return result.IsError ? HandleErrorResponses(result.Errors) : Ok(mapped);
    }

    [HttpPatch]
    [Route(ApiRoutes.ShoppingSession.UpdateQuantity)]
    public async Task<IActionResult> UpdateCartItemQuantity(Guid cartItemId,[FromBody] int quantity,
        CancellationToken cancellationToken)
    {
        var command = new UpdateCartItemQuantityCommand { CartItemId = cartItemId, Quantity = quantity };
        var result = await _mediator.Send(command, cancellationToken);
        var mapped = _mapper.Map<CartItemResponse>(result.Payload);
        return result.IsError ? HandleErrorResponses(result.Errors) : Ok(mapped);
    }

    [HttpDelete]
    [Route(ApiRoutes.ShoppingSession.DeleteItem)]
    public async Task<IActionResult> DeleteCartItem([FromRoute] Guid cartItemId
        , CancellationToken cancellationToken)
    {
        var command = new DeleteItemFromShoppingSessionCommand { CartItemId = cartItemId };
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsError ? HandleErrorResponses(result.Errors) : Ok();
    }
}
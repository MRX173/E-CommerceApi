using Application.Enums;
using Application.Exceptions;
using Application.Models;
using Application.ShoppingSession.Commands;
using Domain.Abstractions;
using Domain.Exceptions;
using Domain.ShoppingSessionAggregate.Entities;
using MediatR;

namespace Application.ShoppingSession.CommandHandlers;

public class AddItemToShoppingSessionHandler :
    IRequestHandler<AddItemToShoppingSessionCommand, OperationResult<CartItem>>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddItemToShoppingSessionHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<CartItem>> Handle(AddItemToShoppingSessionCommand request,
        CancellationToken cancellationToken)
    {
        OperationResult<CartItem> result = new OperationResult<CartItem>();
        try
        {
            CartItem cartItem = CartItem
                .CreateCartItem(request.Quantity,
                    request.ShoppingSessionId, request.ProductId);

            await _unitOfWork
                .ShoppingSessionRepository
                .AddItemToShoppingSession(cartItem);
            try
            {
                await _unitOfWork
                    .SaveChangesAsync(cancellationToken);
            }
            catch (DbCreateException e)
            {
                result
                    .AddError(ErrorCode.DatabaseOperationException
                        , $"CreateCartItem failed Id of cart is {cartItem.Id}");
            }

            // var shoppingSession = await _unitOfWork
            //     .ShoppingSessionRepository
            //     .GetShoppingSessionById(request.ShoppingSessionId);
            // shoppingSession?.GetTotal();
            result.Payload = cartItem;
            return result;
        }
        catch (CartItemNotValidException e)
        {
            e.ValidationErrors.ForEach(x => result.AddError(ErrorCode.CartItemNotValid, e.Message));
        }

        return result;
    }
}
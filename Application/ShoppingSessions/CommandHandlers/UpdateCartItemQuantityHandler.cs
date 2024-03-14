using Application.Enums;
using Application.Exceptions;
using Application.Models;
using Application.ShoppingSession.Commands;
using Domain.Abstractions;
using Domain.Exceptions;
using Domain.ShoppingSessionAggregate.Entities;
using MediatR;

namespace Application.ShoppingSession.CommandHandlers;

public class UpdateCartItemQuantityHandler :
    IRequestHandler<UpdateCartItemQuantityCommand, OperationResult<CartItem>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCartItemQuantityHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<CartItem>> Handle(UpdateCartItemQuantityCommand request,
        CancellationToken cancellationToken)
    {
        OperationResult<CartItem> result = new OperationResult<CartItem>();
        try
        {
            CartItem? cartItem = await _unitOfWork
                .ShoppingSessionRepository
                .GetCartItemById(request.CartItemId);
            if (cartItem is null)
            {
                result
                    .AddError(ErrorCode.CartItemNotFound,
                        "Cart Item Not Found");
                return result;
            }

            cartItem.UpdateQuantityCartItem(request.Quantity);
            await _unitOfWork
                .ShoppingSessionRepository
                .UpdateQuantity(cartItem);
            try
            {
                await _unitOfWork
                    .SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException e)
            {
                result
                    .AddError(ErrorCode.DatabaseOperationException
                        , $"Update CartItem Quantity failed Id of cart is {cartItem.Id}");
            }

            result.Payload = cartItem;
            return result;
        }
        catch (CartItemNotValidException e)
        {
            e.ValidationErrors
                .ForEach(x => result
                    .AddError(ErrorCode.CartItemNotValid, e.Message));
        }

        return result;
    }
}
using Application.Enums;
using Application.Exceptions;
using Application.Models;
using Application.ShoppingSession.Commands;
using Domain.Abstractions;
using Domain.Exceptions;
using Domain.ShoppingSessionAggregate.Entities;
using MediatR;

namespace Application.ShoppingSession.CommandHandlers;

public class DeleteItemFromShoppingSessionHandler :
    IRequestHandler<DeleteItemFromShoppingSessionCommand, OperationResult<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteItemFromShoppingSessionHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<bool>> Handle(DeleteItemFromShoppingSessionCommand request,
        CancellationToken cancellationToken)
    {
        OperationResult<bool> result = new OperationResult<bool>();
        try
        {
            CartItem? cartItem = await _unitOfWork.ShoppingSessionRepository.GetCartItemById(request.CartItemId);
            if (cartItem is null)
            {
                result.AddError(ErrorCode.CartItemNotFound, "Cart Item Not Found");
                return result;
            }

            await _unitOfWork.ShoppingSessionRepository.DeleteItemFromShoppingSession(cartItem);
            try
            {
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch (DbDeleteException e)
            {
                result
                    .AddError(ErrorCode.DatabaseOperationException,
                        $"DeleteCartItem failed Id of cart is {request.CartItemId}");
            }
            result.Payload = true;
            return result;
        }
        catch (CartItemNotValidException e)
        {
            e.ValidationErrors.ForEach(x => result.AddError(ErrorCode.CartItemNotValid, e.Message));
        }
        return result;
    }
}
using Application.Enums;
using Application.Models;
using Application.ShoppingSession.Queries;
using Domain.Abstractions;
using Domain.ShoppingSessionAggregate.Entities;
using MediatR;

namespace Application.ShoppingSession.QueryHandlers;

public class GetCartItemByIdHandler : IRequestHandler<GetCartItemByIdQuery, OperationResult<CartItem>>
{
    private readonly IUnitOfWork  _unitOfWork;

    public GetCartItemByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<OperationResult<CartItem>> Handle(GetCartItemByIdQuery request, CancellationToken cancellationToken)
    {
        OperationResult<CartItem> result = new OperationResult<CartItem>();    
        CartItem? cartItem = await _unitOfWork.ShoppingSessionRepository.GetCartItemById(request.CartItemId);
        if (cartItem == null)
        {
            result.AddError(ErrorCode.CartItemNotFound, "CartItem Not Found");
            return result;
        }   
        result.Payload = cartItem;
        return result;
    }
}
using Application.Abstractions;
using Application.Enums;
using Application.Models;
using Application.Services;
using Application.ShoppingSession.Queries;
using Application.ShoppingSessions.Queries;
using Domain.Abstractions;
using Domain.ShoppingSessionAggregate.Entities;
using MediatR;

namespace Application.ShoppingSession.QueryHandlers;

public class GetShoppingSessionItemsByUserIdHandler :
    IRequestHandler<GetShoppingSessionItemsByUserIdQuery, OperationResult<List<CartItem>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetShoppingSessionItemsByUserIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<List<CartItem>>> Handle(GetShoppingSessionItemsByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        OperationResult<List<CartItem>> result = new OperationResult<List<CartItem>>();
        List<CartItem> cartItems = await _unitOfWork
            .ShoppingSessionRepository
            .GetShoppingSessionItemsByUserId(request.UserId);

        result.Payload = cartItems;
        return result;
    }
}
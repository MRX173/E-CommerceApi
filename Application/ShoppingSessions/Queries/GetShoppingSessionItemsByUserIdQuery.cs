using Application.Models;
using Domain.ShoppingSessionAggregate.Entities;
using MediatR;

namespace Application.ShoppingSessions.Queries;

public class GetShoppingSessionItemsByUserIdQuery : IRequest<OperationResult<List<CartItem>>>
{
    public required Guid? UserId { get; set; } 
}
using Application.Models;
using Domain.ShoppingSessionAggregate.Entities;
using MediatR;

namespace Application.ShoppingSession.Queries;

public class GetCartItemByIdQuery : IRequest<OperationResult<CartItem>>
{
    public Guid CartItemId { get; set; }   
}
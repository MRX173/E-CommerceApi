using Application.Models;
using Domain.ShoppingSessionAggregate.Entities;
using MediatR;

namespace Application.ShoppingSession.Commands;

public class UpdateCartItemQuantityCommand : IRequest<OperationResult<CartItem>>
{
    public Guid CartItemId { get; set; }
    public int Quantity { get; set; }
}
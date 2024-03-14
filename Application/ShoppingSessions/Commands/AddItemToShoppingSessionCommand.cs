using Application.Models;
using Domain.ShoppingSessionAggregate.Entities;
using MediatR;

namespace Application.ShoppingSession.Commands;

public class AddItemToShoppingSessionCommand : IRequest<OperationResult<CartItem>>
{
    public required int Quantity { get; set; }
    public required Guid ProductId { get; set; }
    public required Guid ShoppingSessionId { get; set; }
}
using Application.Models;
using MediatR;

namespace Application.ShoppingSession.Commands;

public class DeleteItemFromShoppingSessionCommand : IRequest<OperationResult<bool>>
{
    public Guid CartItemId { get; set; }
}
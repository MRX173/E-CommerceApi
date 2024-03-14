using Application.Models;
using MediatR;

namespace Application.ShoppingSessions.Commands;

public class CreateShoppingSessionCommand :
    IRequest<OperationResult<Domain.ShoppingSessionAggregate.Entities.ShoppingSession>>
{
    public required Guid? UserId { get; set; }
}
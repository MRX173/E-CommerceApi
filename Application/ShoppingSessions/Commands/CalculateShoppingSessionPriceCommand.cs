using Application.Models;
using MediatR;

namespace Application.ShoppingSessions.Commands;

public class CalculateShoppingSessionPriceCommand
    : IRequest<OperationResult<Domain.ShoppingSessionAggregate.Entities.ShoppingSession>>
{
    public Guid Id { get; set; }
}
using Application.Models;
using Domain.ProductAggregate.Entities;
using MediatR;

namespace Application.Products.Commands;

public class AddProductRateCommand : IRequest<OperationResult<ProductRate>>
{
    public required Guid ProductId { get; set; }
    public required Guid? UserId { get; set; }
    public required int RateValue { get; set; }
}
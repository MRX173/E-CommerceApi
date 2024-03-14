using Application.Models;
using MediatR;

namespace Application.Products.Commands;

public class DeleteProductRateCommand : IRequest<OperationResult<bool>>
{
    public required Guid RateId { get; set; }
    public required Guid ProductId { get; set; }
    public required Guid? UserId { get; set; }
}
using Application.Models;
using Domain.ProductAggregate.Entities;
using MediatR;

namespace Application.Products.Commands;

public class AddProductCommentCommand : IRequest<OperationResult<ProductComment>>
{
    public required Guid ProductId { get; set; }
    public required string Text { get; set; }
    public required Guid? CustumUserId { get; set; }
}
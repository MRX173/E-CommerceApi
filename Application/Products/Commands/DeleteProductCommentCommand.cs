using Application.Models;
using MediatR;

namespace Application.Products.Commands;

public class DeleteProductCommentCommand : IRequest<OperationResult<bool>>
{
    public required Guid ProductId { get; set; }
    public required Guid? UserId { get; set; }
    public required Guid ProductCommentId { get; set; }
}
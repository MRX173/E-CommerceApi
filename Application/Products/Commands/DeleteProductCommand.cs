using Application.Models;
using MediatR;

namespace Application.Products.Commands;

public class DeleteProductCommand : IRequest<OperationResult<bool>>
{
    public required Guid Id { get; set; }
}
using Application.Models;
using MediatR;

namespace Application.ProductCategories.Commands;

public class DeleteProductCategoryCommand : IRequest<OperationResult<bool>>
{
    public required Guid ProductCategoryId { get; set; }
}
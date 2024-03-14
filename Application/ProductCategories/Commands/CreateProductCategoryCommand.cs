using Application.Models;
using Domain.ProductAggregate;
using MediatR;

namespace Application.ProductCategories.Commands;

public class CreateProductCategoryCommand : IRequest<OperationResult<ProductCategory>>
{
    public required string CategoryName { get; set; }
    public required string CategoryDescription { get; set; }
    public required string CategoryImageUrl { get; set; }
}
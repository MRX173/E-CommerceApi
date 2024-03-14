using Application.Models;
using Domain.ProductAggregate.Entities;
using MediatR;

namespace Application.Products.Queries;

public class GetProductsByCategoryNameQuery : IRequest<OperationResult<List<Product>>>
{
    public string CategoryName { get; set; } = string.Empty;
}
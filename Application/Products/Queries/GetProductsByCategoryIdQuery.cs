using Application.Models;
using Domain.ProductAggregate.Entities;
using MediatR;

namespace Application.Products.Queries;

public class GetProductsByCategoryIdQuery : IRequest<OperationResult<List<Product>>>
{
    public Guid CategoryId { get; set; }
}
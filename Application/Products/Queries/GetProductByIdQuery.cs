using Application.Models;
using Domain.ProductAggregate.Entities;
using MediatR;

namespace Application.Products.Queries;

public class GetProductByIdQuery : IRequest<OperationResult<Product>>
{
    public Guid ProductId { get; set; }
}
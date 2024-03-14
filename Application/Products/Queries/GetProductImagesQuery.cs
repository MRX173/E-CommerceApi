using Application.Models;
using Domain.ProductAggregate;
using Domain.ProductAggregate.Entities;
using MediatR;

namespace Application.Products.Queries;

public class GetProductImagesQuery : IRequest<OperationResult<List<ProductImages>>>
{
    public Guid ProductId { get; set; }
}
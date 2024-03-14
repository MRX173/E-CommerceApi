using Application.Models;
using Domain.ProductAggregate.Entities;
using MediatR;

namespace Application.Products.Commands;

public class UpdateProductCommand : IRequest<OperationResult<Product>>
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string MainImage { get; set; }
    public required decimal Price { get; set; }
    public required string Currency { get; set; }
    public required int Stock { get; set; }
}
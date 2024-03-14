using Application.Models;
using Domain.ProductAggregate;
using Domain.ProductAggregate.Entities;
using MediatR;

namespace Application.Products.Commands;

public class CreateProductCommand : IRequest<OperationResult<Product>>
{
    public required string Name { get; set; } 
    public required string Description { get; set; }
    public required string MainImage { get; set; }
    public required decimal Price { get; set; }
    public required string Currency { get; set; }
    public required int Stock { get; set; }
    public required string SkuCode { get; set; }
    public required string CategoryName { get; set; }
    //public required List<ProductImages> ProductImages { get; set; }
}
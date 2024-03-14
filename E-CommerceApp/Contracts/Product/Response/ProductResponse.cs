using Domain.ProductAggregate;

namespace E_CommerceApp.Contracts.Product.Response;

public class ProductResponse
{
    public required Guid Id { get; set; }
    public required string Name { get; set; } 
    public required string Description { get; set; }
    public required string MainImage { get; set; }
    public required decimal Price { get; set; }
    public required string Currency { get; set; }
    public required int Stock { get; set; }
    public required string SkuCode { get; set; }
    public required string CategoryName { get; set; }
    public required List<ProductImages> ProductImages { get; set; } = new();
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset LastModified { get; set; }
}
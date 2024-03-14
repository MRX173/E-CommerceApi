using Domain.ProductAggregate;

namespace E_CommerceApp.Contracts.Product.Request;

public class ProductCreate
{
    public required string Name { get; set; } 
    public required string Description { get; set; }
    public required string MainImage { get; set; }
    public required decimal Price { get; set; }
    public required string Currency { get; set; }
    public required int Stock { get; set; }
    public required string SkuCode { get; set; }
    public required string CategoryName { get; set; }
    //public required List<ProductImageRequest> ProductImages { get; set; } = new();
}
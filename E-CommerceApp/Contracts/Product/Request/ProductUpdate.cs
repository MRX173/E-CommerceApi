namespace E_CommerceApp.Contracts.Product.Request;

public class ProductUpdate
{
    public required string Name { get; set; } 
    public required string Description { get; set; }
    public required string MainImage { get; set; }
    public required decimal Price { get; set; }
    public required string Currency { get; set; }
    public required int Stock { get; set; }
    public required string SkuCode { get; set; }
}
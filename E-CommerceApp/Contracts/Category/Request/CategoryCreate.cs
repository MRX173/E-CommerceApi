namespace E_CommerceApp.Contracts.Product.Request;

public class CategoryCreate
{
    public required string  CategoryName { get; set; }
    public required string Description { get; set; }
    public required string ImageUrl { get; set; }
}
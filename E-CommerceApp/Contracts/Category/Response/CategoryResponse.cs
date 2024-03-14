namespace E_CommerceApp.Contracts.Product.Response;

public class CategoryResponse
{
    public required Guid Id { get; set; }
    public required string CategoryName { get; set; }
    public required string Description { get; set; }
    public required string ImageUrl { get; set; }
    public required DateTimeOffset Created { get; set; }
    public required DateTimeOffset LastModified { get; set; }
    public required DateTimeOffset Deleted { get; set; }
}
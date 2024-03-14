namespace E_CommerceApp.Contracts.Product.Response;

public class CommentResponse
{
    public required Guid ProductId { get; set; }
    public required string Text { get; set; }
    public required string UserId { get; set; }
    public required DateTimeOffset Created { get; set; }
    public required DateTimeOffset LastModified { get; set; }
}
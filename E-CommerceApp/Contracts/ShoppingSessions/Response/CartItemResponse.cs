namespace E_CommerceApp.Contracts.ShoppingSessions.Response;

public class CartItemResponse
{
    public int Quantity { get; set; }
    public Guid CartItemId { get; set; }
    public Guid ShoppingSessionId { get; set; }
    public Guid ProductId { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset LastModified { get; set; }
}
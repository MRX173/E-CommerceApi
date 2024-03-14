namespace E_CommerceApp.Contracts.ShoppingSessions.Request;

public class AddItemRequest
{
    public int Quantity { get; set; }
    public Guid ProductId { get; set; }
    public Guid ShoppingSessionId { get; set; }
}
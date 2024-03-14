namespace E_CommerceApp.Contracts.Order.Request;

public class OrderItemRequest
{
    public required int Quantity { get; set; }
    public required Guid ProductId { get; set; }
    public required Guid OrderDetailsId { get; set; }
}
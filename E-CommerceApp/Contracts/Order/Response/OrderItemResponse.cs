namespace E_CommerceApp.Contracts.Order.Response;

public class OrderItemResponse
{
    public Guid OrderDetailsId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset LastModified { get; set; }
}
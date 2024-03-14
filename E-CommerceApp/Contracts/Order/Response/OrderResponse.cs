namespace E_CommerceApp.Contracts.Order.Response;

public class OrderResponse
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public string UserId { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset LastModified { get; set; }
}
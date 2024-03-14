namespace E_CommerceApp.Contracts.ShoppingSessions.Response;

public class ShoppingSessionResponse
{
    public Guid Id { get; set; }
    public decimal Value { get; set; }
    public string  Currency { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset LastModified { get; set; }
}
using Domain.Enums;

namespace E_CommerceApp.Contracts.UserPayments.Response;

public class UserPaymentResponse
{
    public string CardNumber { get; set; }
    public string Provider { get; set; }
    public string PaymentType { get; set; }
    public DateTimeOffset ExpirationDate { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset LastModified { get; set; }
}
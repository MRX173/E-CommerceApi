using Domain.Enums;

namespace E_CommerceApp.Contracts.UserPayments.Request;

public class UserPaymentCreate
{
    public required string CardNumber { get; set; }
    public required string Provider { get; set; }
    public required string PaymentType { get; set; }
    public required DateTimeOffset ExpirationDate { get; set; }
}
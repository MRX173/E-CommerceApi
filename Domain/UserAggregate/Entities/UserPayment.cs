using Domain.Common;
using Domain.Enums;
using Domain.Exceptions.UserExceptions;
using Domain.OrderAggregate.Entities;
using Domain.Validators.UserValidators;
using FluentValidation.Results;

namespace Domain.UserAggregate.Entities;

public class UserPayment : AuditableEntityWithoutDeletion
{
    private UserPayment()
    {
    }

    public PaymentType PaymentType { get; private set; }
    public Provider Provider { get; private set; }
    public string CardNumber { get; set; } = string.Empty;
    public DateTimeOffset ExpirationDate { get; private set; }
    public Guid CustumUserId { get; private set; }
    public CustumUser? CustumUser { get; set; }

    public static UserPayment? CreateUserPayment(Guid userId, PaymentType paymentPaymentType, Provider provider,
        string cardNumber, DateTimeOffset expirationDate)
    {
        UserPaymentValidator validator = new UserPaymentValidator();
        UserPayment? userPaymentToValidate = new UserPayment
        {
            Id = Guid.NewGuid(),
            CustumUserId = userId,
            PaymentType = paymentPaymentType,
            Provider = provider,
            CardNumber = cardNumber,
            ExpirationDate = expirationDate,
        };
        ValidationResult? validationResult = validator.Validate(userPaymentToValidate);
        if (validationResult.IsValid) return userPaymentToValidate;
        UserPaymentNotValidException exception = new UserPaymentNotValidException("User payment is not valid");
        validationResult.Errors.ForEach(error => exception.ValidationErrors.Add(error.ErrorMessage));
        throw exception;
    }


    public void UpdateUserPayment(PaymentType paymentPaymentType, Provider provider,
        string cardNumber, DateTimeOffset expirationDate)
    {
        if (string.IsNullOrEmpty(cardNumber))
        {
            UserPaymentNotValidException exception = new UserPaymentNotValidException("User payment is not valid");
            exception.ValidationErrors.Add("card number can't be null or empty");
        }

        PaymentType = paymentPaymentType;
        Provider = provider;
        CardNumber = cardNumber;
        ExpirationDate = expirationDate;
    }
}
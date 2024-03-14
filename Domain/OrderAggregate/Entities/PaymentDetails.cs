using Domain.Common;
using Domain.CommonValueObject;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Validators.OrderValidators;
using FluentValidation.Results;

namespace Domain.OrderAggregate.Entities;

public class PaymentDetails : AuditableEntityWithoutDeletion
{
    private PaymentDetails()
    {
    }

    public Price Amount { get; private set; }
    public Provider Provider { get; private set; }
    public Status PaymentStatus { get; private set; }
    public Guid OrderDetailsId { get; private set; }
    public OrderDetails OrderDetails { get; private set; }

    public static PaymentDetails CreatePaymentDetails(Price amount, Provider provider
        , Status paymentStatus, Guid orderDetailsId)
    {
        PaymentDetailsValidator validator = new PaymentDetailsValidator();
        PaymentDetails paymentDetailsToValidate = new PaymentDetails
        {
            Id = Guid.NewGuid(),
            Amount = amount,
            Provider = provider,
            PaymentStatus = paymentStatus,
            OrderDetailsId = orderDetailsId,
            Created = DateTimeOffset.UtcNow,
            LastModified = DateTimeOffset.UtcNow
        };
        ValidationResult? validationResult = validator.Validate(paymentDetailsToValidate);
        if (validationResult.IsValid) return paymentDetailsToValidate;
        PriceLessThanZeroException exception = new PriceLessThanZeroException("Payment details is not valid");
        validationResult.Errors.ForEach(error => exception.ValidationErrors.Add(error.ErrorMessage));
        throw exception;
    }
}
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Exceptions.ProductExceptions;
using Domain.UserAggregate.Entities;
using Domain.Validators.ProductValidators;
using FluentValidation.Results;

namespace Domain.ProductAggregate.Entities;

public class ProductRate : AuditableEntityWithoutDeletion
{
    private ProductRate()
    {
    }

    public int RateValue { get; private set; }
    public Guid ProductId { get; private set; }
    public Guid? CustumUserId { get; private set; } 
    public CustumUser? CustumUser { get; private set; }
    public Product Product { get; private set; }

    public static ProductRate CreateProductRate(int rateValue, Guid productId, Guid? custumUserId)
    {
        var validator = new ProductRateValidator();
        var productRateToValidate = new ProductRate
        {
            Id = Guid.NewGuid(),
            RateValue = rateValue,
            ProductId = productId,
            CustumUserId = custumUserId,
            Created = DateTimeOffset.UtcNow,
            LastModified = DateTimeOffset.UtcNow
        };
        ValidationResult? validationResult = validator.Validate(productRateToValidate);
        if (validationResult.IsValid) return productRateToValidate;
        ProductRateNotValidException exception = new ProductRateNotValidException("Product rate is not valid");
        validationResult.Errors.ForEach(error => exception.ValidationErrors.Add(error.ErrorMessage));
        throw exception;
    }

    public void UpdateProductRate(int rateValue)
    {
        if (RateValue == rateValue) return;
        RateValue = rateValue;
    }
}
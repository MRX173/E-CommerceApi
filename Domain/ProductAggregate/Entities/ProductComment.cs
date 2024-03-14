using Domain.Common;
using Domain.Exceptions.ProductExceptions;
using Domain.UserAggregate.Entities;
using Domain.Validators.ProductValidators;
using FluentValidation.Results;

namespace Domain.ProductAggregate.Entities;

public class ProductComment : AuditableEntityWithoutDeletion
{
    private ProductComment() {}
    
    public string Text { get; private set; } = string.Empty;
    public Guid ProductId { get; private set; }
    public Guid? CustumUserId { get; private set; }
    public CustumUser CustumUser { get; private set; }
    public Product Product { get; private set; }

    public static ProductComment CreateProductComment(string text, Guid productId, Guid? custumUserId)
    {
        var validator = new ProductCommentValidator();
        var productCommentToValidate = new ProductComment
        {
            Id = Guid.NewGuid(),
            Text = text,
            ProductId = productId,
            CustumUserId = custumUserId,
            Created = DateTimeOffset.UtcNow,
            LastModified = DateTimeOffset.UtcNow
        };
        ValidationResult? validationResult = validator.Validate(productCommentToValidate);
        if (validationResult.IsValid) return productCommentToValidate;
        ProductCommentNotValidException exception = new ProductCommentNotValidException("Product comment is not valid");
        validationResult.Errors.ForEach(error => exception.ValidationErrors.Add(error.ErrorMessage));
        throw exception;
    }

    public void UpdateProductComment(string text)
    {
        if (Text == text) return;
        Text = text;
    }
}
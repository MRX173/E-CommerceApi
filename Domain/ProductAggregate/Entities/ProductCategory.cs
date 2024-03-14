using Domain.Common;
using Domain.Exceptions;
using Domain.Exceptions.ProductExceptions;
using Domain.ProductAggregate.Entities;
using Domain.Validators.ProductValidators;
using FluentValidation.Results;

namespace Domain.ProductAggregate;

public class ProductCategory : BaseAuditableEntity
{
    private readonly List<Product> _products = new List<Product>();

    #region PrivateConstructor

    private ProductCategory()
    {
    }

    #endregion


    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string ImageUrl { get; private set; } = string.Empty;

    public IEnumerable<Product> Products
    {
        get { return _products; }
    }

    public static ProductCategory CreateProductCategory(string name, string description, string imageUrl)
    {
        ProductCategoryValidator validator = new ProductCategoryValidator();
        ProductCategory productCategoryToValidate = new ProductCategory
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            ImageUrl = imageUrl,
            Created = DateTimeOffset.UtcNow,
            LastModified = DateTimeOffset.UtcNow
        };
        ValidationResult? validationResult = validator.Validate(productCategoryToValidate);
        if (validationResult.IsValid) return productCategoryToValidate;
        ProductCategoryNotValidException exception = new ProductCategoryNotValidException("Product category is not valid");
        validationResult.Errors.ForEach(error => exception.ValidationErrors.Add(error.ErrorMessage));
        throw exception;
    }

    public ProductCategory UpdateProductCategory(string name, string description, string imageUrl)
    {
        if (name == Name && description == Description && imageUrl == ImageUrl)
        {
            ProductCategoryNotValidException exception = new ProductCategoryNotValidException("Product category update is not valid");
            exception.ValidationErrors.Add("Product category is not changed");
        }

        Name = name;
        Description = description;
        ImageUrl = imageUrl;
        LastModified = DateTimeOffset.UtcNow;

        return this;
    }
}
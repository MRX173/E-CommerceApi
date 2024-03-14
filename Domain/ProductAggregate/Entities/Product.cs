using Domain.Common;
using Domain.CommonValueObject;
using Domain.Exceptions.ProductExceptions;
using Domain.Validators.ProductValidators;
using FluentValidation.Results;

namespace Domain.ProductAggregate.Entities;

public class Product : BaseAuditableEntity
{
    private readonly List<ProductImages> _productImages = new List<ProductImages>();
    private readonly List<ProductRate> _productRate = new List<ProductRate>();
    private readonly List<ProductComment?> _productComments = new List<ProductComment?>();

    #region PrivateConstructor

    private Product()
    {
        DefaultDiscountOfProduct();
    }

    #endregion

    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string MainImage { get; private set; }
    public Price Price { get; private set; }
    public Discount Discount { get; private set; }
    public Sku Sku { get; private set; } // TODO: Add SKU code automatically
    public ProductInventory Inventory { get; private set; }
    public Guid ProductCategoryId { get; private set; }
    public ProductCategory Category { get; private set; }

    public IEnumerable<ProductComment?> ProductComments
    {
        get { return _productComments; }
    }

    public IEnumerable<ProductImages> ProductImages
    {
        get { return _productImages; }
    }

    public IEnumerable<ProductRate> ProductRate
    {
        get { return _productRate; }
    }


    public static Product CreateProduct(string name, string description, string mainImage
        , Price price, Sku sku,
        ProductInventory inventory,
        Guid productCategoryId)
    {
        ProductValidator validator = new ProductValidator();

        Product productToValidate = new Product
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            MainImage = mainImage,
            Price = price,
            Sku = sku,
            Inventory = inventory,
            ProductCategoryId = productCategoryId,
            Created = DateTimeOffset.UtcNow,
            LastModified = DateTimeOffset.UtcNow
        };
        ValidationResult? validationResult = validator.Validate(productToValidate);
        if (validationResult.IsValid) return productToValidate;
        ProductNotValidException exception = new ProductNotValidException("Product is not valid");
        validationResult.Errors.ForEach(error => exception.ValidationErrors.Add(error.ErrorMessage));
        throw exception;
    }

    public Product UpdateProduct(string name, string description
        , string mainImage, ProductInventory inventory, Price price)
    {
        if (string.IsNullOrEmpty(name)
            || string.IsNullOrEmpty(description)
            || inventory is null || price is null)
        {
            ProductNotValidException exception =
                new ProductNotValidException("Can't update product" + "Product is not valid");
            exception.ValidationErrors.Add("name can't be null or empty");
            throw exception;
        }

        Name = name;
        Description = description;
        MainImage = mainImage;
        Inventory = inventory;
        Price = price;
        LastModified = DateTimeOffset.UtcNow;
        return this;
    }

    public void SetDiscountForProduct(decimal percentage, bool isActiveDiscount)
    {
        this.Discount = Discount.CreateDiscount(percentage, isActiveDiscount);
    }

    public void UpdateActivationDiscountOfProduct(bool isActivationDiscount)
    {
        this.Discount.UpdateActivationOfDiscount(isActivationDiscount);
    }

    public void UpdatePercentageDiscountOfProduct(decimal percentage)
    {
        this.Discount.UpdatePercentageOfDiscount(percentage);
    }


    public void UpdatePriceOfProduct(decimal value, string currency)
    {
        this.Price.UpdatePrice(value, currency);
    }


    public void UpdateQuantity(int quantity)
    {
        this.Inventory.UpdateQuantity(quantity);
    }

    public void AddProductImages(List<ProductImages> productImages)
    {
        _productImages.AddRange(productImages);
    }

    public void AddProductRate(ProductRate productRate)
    {
        _productRate.Add(productRate);
    }
    public void AddProductComments(ProductComment? productComments)
    {
        _productComments.Add(productComments);
    }

    public void DefaultDiscountOfProduct()
    {
        this.Discount = Discount.CreateDiscount(0, false);
    }
}
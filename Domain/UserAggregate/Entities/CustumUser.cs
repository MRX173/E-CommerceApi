using Domain.Exceptions;
using Domain.Exceptions.UserExceptions;
using Domain.OrderAggregate;
using Domain.ProductAggregate.Entities;
using Domain.ShoppingSessionAggregate.Entities;
using Domain.UserAggregate.ValueObjects;
using Domain.Validators.UserValidators;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;

namespace Domain.UserAggregate.Entities;

public sealed class CustumUser : IdentityUser<Guid>
{
    private readonly List<OrderDetails> _orders = new List<OrderDetails>();
    private readonly List<UserPayment> _paymentMethods = new List<UserPayment>();
    private readonly List<ShoppingSession> _shoppingSessions = new List<ShoppingSession>();
    private readonly List<ProductRate> _productRate = new List<ProductRate>();
    private readonly List<ProductComment> _productComments = new List<ProductComment>();

    #region PrivateConstructor

    private CustumUser()
    {
        this.UserName =FirstName + LastName;
    }

    #endregion

    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public UserAddress? UserAddress { get; private set; }
    public DateTimeOffset Created { get; private set; }
    public DateTimeOffset LastModified { get; private set; }

    public ICollection<ShoppingSession> ShoppingSessions
    {
        get { return _shoppingSessions; }
    }

    public ICollection<OrderDetails> Orders
    {
        get { return _orders; }
    }

    public ICollection<UserPayment> PaymentMethods
    {
        get { return _paymentMethods; }
    }

    public ICollection<ProductRate> ProductRate
    {
        get { return _productRate; }
    }

    public ICollection<ProductComment> ProductComments
    {
        get { return _productComments; }
    }


    public static CustumUser? CreateUser(string firstName, string lastName, UserAddress userAddress
        , string EmailAddress, string phoneNumber)
    {
        CustumUserValidator validator = new CustumUserValidator();

        CustumUser? userToValidate = new CustumUser
        {
            Id = Guid.NewGuid(),
            FirstName = firstName,
            LastName = lastName,
            UserName = firstName + lastName,
            UserAddress = userAddress,
            Email = EmailAddress,
            PhoneNumber = phoneNumber,
            Created = DateTimeOffset.UtcNow,
            LastModified = DateTimeOffset.UtcNow
        };

        ValidationResult? validationResult = validator.Validate(userToValidate);
        if (validationResult.IsValid) return userToValidate;
        UserNotValidException exception = new UserNotValidException("User is not valid");
        validationResult.Errors.ForEach(error => exception.ValidationErrors.Add(error.ErrorMessage));
        throw exception;
    }

    public CustumUser? UpdateUser(string firstName, string lastName, UserAddress? userAddress, string phoneNumber)
    {
        if (string.IsNullOrEmpty(firstName)
            || string.IsNullOrEmpty(lastName) 
            || string.IsNullOrEmpty(phoneNumber))
        {
            UserNotValidException exception = new UserNotValidException("User is not valid");
            exception.ValidationErrors.Add("Please enter a valid first name, last name, and phone number");
            throw exception;
        }

        FirstName = firstName;
        LastName = lastName;
        UserAddress = userAddress;
        PhoneNumber = phoneNumber;
        LastModified = DateTimeOffset.UtcNow;
        return this;
    }

    public UserAddress? UpdateUserAdderess(string addressLine, string city, string country)
    {
        return this.UserAddress.UpdateUserAddress(addressLine, city, country);
    }
}
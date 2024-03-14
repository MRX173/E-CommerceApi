using Domain.Common;
using Domain.CommonValueObject;
using Domain.Exceptions;
using Domain.Exceptions.OrderExceptions;
using Domain.OrderAggregate.Entities;
using Domain.UserAggregate;
using Domain.UserAggregate.Entities;
using Domain.Validators.OrderValidators;
using FluentValidation.Results;

namespace Domain.OrderAggregate;

public class OrderDetails : AuditableEntityWithoutDeletion
{
    private readonly List<OrderItem> _orderItems = new List<OrderItem>();

    private OrderDetails()
    {
    }

    public Price TotalPrice { get; private set; }
    public Guid CustumUserId { get; private set; }
    public CustumUser CustumUser { get; private set; }
    public Guid PaymentDetailsId { get; private set; }
    public PaymentDetails PaymentDetails { get; private set; }

    public IEnumerable<OrderItem> OrderItems
    {
        get { return _orderItems; }
    }


    public static OrderDetails CreateOrderDetails(Guid userId)
    {
        OrderDetailsValidator validator = new OrderDetailsValidator();
        OrderDetails orderDetailsToValidate = new OrderDetails
        {
            Id = Guid.NewGuid(),
            CustumUserId = userId,
            TotalPrice = Price.Create(0, "USD"),
            Created = DateTimeOffset.UtcNow,
            LastModified = DateTimeOffset.UtcNow
        };

        ValidationResult? validationResult = validator.Validate(orderDetailsToValidate);

        if (validationResult.IsValid) return orderDetailsToValidate;

        OrderDetailsNotValidException exception =
            new OrderDetailsNotValidException("Order details is not valid");

        validationResult.Errors.ForEach(error => exception
            .ValidationErrors.Add(error.ErrorMessage));
        throw exception;
    }

    public void AddOrderItemToOrderDetails(OrderItem orderItem)
    {
        _orderItems.Add(orderItem);
    }
}
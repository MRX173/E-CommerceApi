using Domain.Common;
using Domain.Exceptions;
using Domain.Exceptions.OrderExceptions;
using Domain.ProductAggregate;
using Domain.ProductAggregate.Entities;
using Domain.Validators.OrderValidators;
using FluentValidation.Results;

namespace Domain.OrderAggregate.Entities;

public class OrderItem : AuditableEntityWithoutDeletion
{
    private OrderItem()
    {
    }

    public int Quantity { get; private set; }
    public Guid OrderDetailsId { get; private set; }
    public OrderDetails OrderDetails { get; private set; }
    public Guid ProductId { get; private set; }
    public Product Product { get; private set; }

    public static OrderItem CreateOrderItem(int quantity, Guid orderDetailsId, Guid productId)
    {
        OrderItemValidator validator = new OrderItemValidator();
        OrderItem orderItemToValidate = new OrderItem
        {
            Id = Guid.NewGuid(),
            Quantity = quantity,
            OrderDetailsId = orderDetailsId,
            ProductId = productId,
            Created = DateTimeOffset.UtcNow,
            LastModified = DateTimeOffset.UtcNow
        };
        ValidationResult? validationResult = validator.Validate(orderItemToValidate);
        if (validationResult.IsValid) return orderItemToValidate;
        OrderItemNotValidException exception = new OrderItemNotValidException("Order item is not valid");
        validationResult.Errors.ForEach(error => exception.ValidationErrors.Add(error.ErrorMessage));
        throw exception;
    }

    public void UpdateQuantityOrderItem(int quantity)
    {
        this.Quantity = quantity;
    }
}
using Application.Enums;
using Application.Exceptions;
using Application.Models;
using Application.Orders.Commands;
using Domain.Abstractions;
using Domain.Exceptions.OrderExceptions;
using Domain.OrderAggregate.Entities;
using MediatR;

namespace Application.Orders.CommandHandlers;

public class AddOrderItemHandler : IRequestHandler<AddOrderItemCommand, OperationResult<OrderItem>>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddOrderItemHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<OrderItem>> Handle(AddOrderItemCommand request,
        CancellationToken cancellationToken)
    {
        var result = new OperationResult<OrderItem>();
        try
        {
            var orderItem = OrderItem
                .CreateOrderItem(request.Quantity, request.OrderDetailsId, request.ProductId);
            await _unitOfWork.OrderRepository.AddOrderItem(orderItem);
            try
            {
                await _unitOfWork
                    .SaveChangesAsync(cancellationToken);
            }
            catch (DbCreateException e)
            {
                e.ValidationErrors
                    .ForEach(x => result
                        .AddError(ErrorCode.DatabaseOperationException,
                            "CreateOrderItem failed"));
            }

            result.Payload = orderItem;
            return result;
        }
        catch (OrderItemNotValidException e)
        {
            e.ValidationErrors.ForEach(x => result.AddError(ErrorCode.OrderItemNotValid, e.Message));
        }

        return result;
    }
}
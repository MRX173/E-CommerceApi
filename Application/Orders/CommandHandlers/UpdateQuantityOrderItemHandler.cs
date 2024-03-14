using Application.Enums;
using Application.Exceptions;
using Application.Models;
using Application.Orders.Commands;
using Domain.Abstractions;
using Domain.Exceptions.OrderExceptions;
using Domain.OrderAggregate.Entities;
using MediatR;

namespace Application.Orders.CommandHandlers;

public class UpdateQuantityOrderItemHandler : IRequestHandler<UpdateQuantityOrderItemCommand, OperationResult<OrderItem>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateQuantityOrderItemHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<OrderItem>> Handle(UpdateQuantityOrderItemCommand request,
        CancellationToken cancellationToken)
    {
        var result = new OperationResult<OrderItem>();
        try
        {
            var order = await _unitOfWork.OrderRepository.GetOrderItemById(request.OrderItemId);
            if (order == null)
            {
                result.AddError(ErrorCode.NotFound, "Order item not found");
                return result;
            }

            order.UpdateQuantityOrderItem(request.Quantity);
            await _unitOfWork.OrderRepository.UpdateQuantityOrderItem(order);
            try
            {
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch (DbCreateException e)
            {
                e.ValidationErrors
                    .ForEach(x => result
                        .AddError(ErrorCode.DatabaseOperationException,
                            "UpdateQuantityOrderItem failed"));
            }

            result.Payload = order;
            return result;
        }
        catch (OrderItemNotValidException e)
        {
            e.ValidationErrors
                .ForEach(x => result
                    .AddError(ErrorCode.OrderDetailsNotValid, e.Message));
        }

        return result;
    }
}
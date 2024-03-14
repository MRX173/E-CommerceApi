using Application.Enums;
using Application.Exceptions;
using Application.Models;
using Application.Orders.Commands;
using Domain.Abstractions;
using Domain.OrderAggregate;
using MediatR;

namespace Application.Orders.CommandHandlers;

public class DeleteOrderItemHandler : IRequestHandler<DeleteOrderItemCommand, OperationResult<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteOrderItemHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<bool>> Handle(DeleteOrderItemCommand request,
        CancellationToken cancellationToken)
    {
        var result = new OperationResult<bool>();
        try
        {
            var order = await _unitOfWork
                .OrderRepository
                .GetOrderItemById(request.OrderId);
            if (order == null)
            {
                result.AddError(ErrorCode.NotFound, "Order item not found");
                return result;
            }

            await _unitOfWork
                .OrderRepository
                .DeleteOrderItem(order);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            result.Payload = true;
            return result;
        }
        catch (DbCreateException e)
        {
            e.ValidationErrors
                .ForEach(x => result
                    .AddError(ErrorCode.DatabaseOperationException
                        , "DeleteOrderItem failed"));
        }

        return result;
    }
}
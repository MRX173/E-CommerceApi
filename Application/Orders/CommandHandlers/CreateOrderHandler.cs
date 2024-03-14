using Application.Enums;
using Application.Exceptions;
using Application.Models;
using Application.Orders.Commands;
using Domain.Abstractions;
using Domain.CommonValueObject;
using Domain.Exceptions.OrderExceptions;
using Domain.OrderAggregate;
using MediatR;

namespace Application.Orders.CommandHandlers;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, OperationResult<OrderDetails>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateOrderHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<OrderDetails>> Handle(CreateOrderCommand request,
        CancellationToken cancellationToken)
    {
        var result = new OperationResult<OrderDetails>();
        try
        {
            var order = OrderDetails.CreateOrderDetails(request.UserId);
            await _unitOfWork.OrderRepository.CreateOrder(order);
            try
            {
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch (DbCreateException e)
            {
                e.ValidationErrors
                    .ForEach(x => result
                        .AddError(ErrorCode.DatabaseOperationException, "CreateOrder failed"));
            }

            result.Payload = order;
            return result;
        }
        catch (OrderDetailsNotValidException e)
        {
            e.ValidationErrors.ForEach(x => result.AddError(ErrorCode.OrderDetailsNotValid, e.Message));
        }

        return result;
    }
}
using Application.Enums;
using Application.Models;
using Application.Orders.Queries;
using Domain.Abstractions;
using Domain.OrderAggregate;
using MediatR;

namespace Application.Orders.QueryHandler;

public class GetOrderDetailsByIdHandler : IRequestHandler<GetOrderDetailsByIdQuery, OperationResult<OrderDetails>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetOrderDetailsByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<OrderDetails>> Handle(GetOrderDetailsByIdQuery request,
        CancellationToken cancellationToken)
    {
        var result = new OperationResult<OrderDetails>();
        var order = await _unitOfWork.OrderRepository.GetOrderDetailsById(request.OrderId);
        if (order == null)
        {
            result.AddError(ErrorCode.NotFound,"OrderDetails not found");
            return result;
        }
        result.Payload = order;
        return result;
    }
}
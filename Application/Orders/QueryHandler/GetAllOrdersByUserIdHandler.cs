using Application.Models;
using Application.Orders.Queries;
using Domain.Abstractions;
using Domain.OrderAggregate;
using MediatR;

namespace Application.Orders.QueryHandler;

public class
    GetAllOrdersByUserIdHandler : IRequestHandler<GetAllOrdersByUserIdQuery, OperationResult<List<OrderDetails>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllOrdersByUserIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<List<OrderDetails>>> Handle(GetAllOrdersByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        var result = new OperationResult<List<OrderDetails>>();
        var orders = await _unitOfWork
            .OrderRepository
            .GetAllOrdersByUserId(request.UserId);
        result.Payload = orders;
        return result;
    }
}
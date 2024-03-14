using Application.Enums;
using Application.Models;
using Application.UserPayments.Queries;
using Domain.Abstractions;
using Domain.UserAggregate.Entities;
using MediatR;

namespace Application.UserPayments.QueryHandlers;

public class GetUserPaymentsByUserIdHandler
    : IRequestHandler<GetUserPaymentsByUserIdQuery, OperationResult<List<UserPayment?>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUserPaymentsByUserIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<List<UserPayment?>>> Handle(GetUserPaymentsByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        var result = new OperationResult<List<UserPayment?>>();
        var userPayments = await _unitOfWork.UserPaymentRepository
            .GetUserPaymentsByUserId(request.UserId);
        if (userPayments == null)
        {
            result.AddError(ErrorCode.NotFound, "User payments not found");
            return result;
        }

        result.Payload = userPayments;
        return result;
    }
}
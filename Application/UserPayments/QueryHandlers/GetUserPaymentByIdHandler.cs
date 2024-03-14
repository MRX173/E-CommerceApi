using Application.Enums;
using Application.Models;
using Application.UserPayments.Queries;
using Domain.Abstractions;
using Domain.UserAggregate.Entities;
using MediatR;

namespace Application.UserPayments.QueryHandlers;

public class GetUserPaymentByIdHandler : IRequestHandler<GetUserPaymentByIdQuery, OperationResult<UserPayment>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUserPaymentByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<UserPayment>> Handle(GetUserPaymentByIdQuery request,
        CancellationToken cancellationToken)
    {
        var result = new OperationResult<UserPayment>();
        try
        {
            var userPayment = await _unitOfWork.UserPaymentRepository
                .GetUserPaymentById(request.UserPaymentId);
            if (userPayment is null)
            {
                result.AddError(ErrorCode.NotFound, "User payment not found");
                return result;
            }

            result.Payload = userPayment;
            return result;
        }
        catch (Exception e)
        {
            result.AddError(ErrorCode.UnknownError, e.Message);
            return result;
        }

        return result;
    }
}
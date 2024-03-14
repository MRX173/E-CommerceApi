using Application.Enums;
using Application.Exceptions;
using Application.Models;
using Application.UserPayments.Commands;
using Domain.Abstractions;
using Domain.Exceptions.UserExceptions;
using Domain.UserAggregate.Entities;
using MediatR;

namespace Application.UserPayments.CommandHandlers;

public class CreateUserPaymentHandler : IRequestHandler<CreateUserPaymentCommand, OperationResult<UserPayment>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserPaymentHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<UserPayment>> Handle(CreateUserPaymentCommand request,
        CancellationToken cancellationToken)
    {
        var result = new OperationResult<UserPayment>();
        try
        {
            var userPayment = UserPayment
                .CreateUserPayment(request.UserId,request.PaymentType, request.Provider
                    ,request.CardNumber,request.ExpirationDate);
            await _unitOfWork.UserPaymentRepository.CreateUserPayment(userPayment);
            try
            {
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch (DbCreateException e)
            {
                result.AddError(ErrorCode.DatabaseOperationException , "CreateUserPayment operation failed");
            }
        }
        catch (UserPaymentNotValidException e)
        {
            e.ValidationErrors
                .ForEach(x => result
                    .AddError(ErrorCode.ValidationError,
                        $"Invalid user payment data: {x}"));
        }

        return result;
    }
}
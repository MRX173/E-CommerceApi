using Application.Enums;
using Application.Exceptions;
using Application.Models;
using Application.UserPayments.Commands;
using Domain.Abstractions;
using Domain.UserAggregate.Entities;
using MediatR;

namespace Application.UserPayments.CommandHandlers;

public class DeleteUserPaymentHandler : IRequestHandler<DeleteUserPaymentCommand, OperationResult<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserPaymentHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<bool>> Handle(DeleteUserPaymentCommand request,
        CancellationToken cancellationToken)
    {
        var result = new OperationResult<bool>();
        try
        {
            var userPayment = await _unitOfWork.UserPaymentRepository
                .GetUserPaymentById(request.UserPaymentId);
            await _unitOfWork.UserPaymentRepository.DeleteUserPayment(userPayment);
            try
            {
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException e)
            {
                result.AddError(ErrorCode.DatabaseOperationException, "Delete UserPayment operation failed");
            }

            result.Payload = true;
            return result;
        }
        catch (Exception e)
        {
            result.AddError(ErrorCode.UnknownError, "Delete UserPayment operation failed");
        }

        return result;
    }
}
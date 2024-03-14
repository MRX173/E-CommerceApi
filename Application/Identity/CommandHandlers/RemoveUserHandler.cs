using Application.Enums;
using Application.Identity.Commands;
using Application.Models;
using Domain.Abstractions;
using Domain.UserAggregate.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Identity.CommandHandlers;

public class RemoveUserHandler : IRequestHandler<RemoveUserCommand, OperationResult<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private OperationResult<bool> _result = new();

    public RemoveUserHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<bool>> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            CustumUser? user = await _unitOfWork.UserRepository.GetUserById(request.UserId);
            if (user == null)
                _result.AddError(ErrorCode.UserDoesNotExist, "User not found");
            Task result = _unitOfWork.UserRepository.DeleteUser(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            _result.Payload = true;
            return _result;
        }
        catch (Exception e)
        {
            _result.AddError(ErrorCode.UnknownError, e.Message);
        }

        return _result;
    }
}
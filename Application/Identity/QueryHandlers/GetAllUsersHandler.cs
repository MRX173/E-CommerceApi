using Application.Enums;
using Application.Identity.Queries;
using Application.Models;
using Domain.Abstractions;
using Domain.UserAggregate.Entities;
using MediatR;

namespace Application.Identity.QueryHandlers;

public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, OperationResult<List<CustumUser>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllUsersHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<List<CustumUser>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        // TODO Error handling
        OperationResult<List<CustumUser>> result = new OperationResult<List<CustumUser>>();
        List<CustumUser>? users = await _unitOfWork.UserRepository.GetAllUsers();
        if (users is null)
        {
            result.AddError(ErrorCode.NotFound, "Users Not Found");
            return result;
        }
        result.Payload = users;
        return result;
    }
}
using Application.Enums;
using Application.Identity.Queries;
using Application.Models;
using Domain.Abstractions;
using Domain.UserAggregate.Entities;
using MediatR;

namespace Application.Identity.QueryHandlers;

public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, OperationResult<CustumUser>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUserByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<CustumUser>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        OperationResult<CustumUser> result = new OperationResult<CustumUser>();
        CustumUser? user = await _unitOfWork.UserRepository.GetUserById(request.UserId);
        if (user is null)
        {
            result.AddError(ErrorCode.NotFound, "User Not Found");
            return result;
        }

        result.Payload = user;
        return result;
    }
}
using Application.Enums;
using Application.Models;
using Application.Roles.Queries;
using Domain.Abstractions;
using Domain.UserAggregate.Entities;
using MediatR;

namespace Application.Roles.QueryHandlers;

public class GetRoleByIdHandler : IRequestHandler<GetRoleByIdQuery, OperationResult<UserRole>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetRoleByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<UserRole>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<UserRole>();
        var role = await _unitOfWork.RoleRepository.GetRoleById(request.Id);
        if (role == null)
        {
            result.AddError(ErrorCode.NotFound, "Role not found");
            return result;
        }

        result.Payload = role;
        return result;
    }
}
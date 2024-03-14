using Application.Enums;
using Application.Models;
using Application.Roles.Commands;
using Domain.Abstractions;
using Domain.UserAggregate.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Roles.CommandHandlers;

public class UpdateRoleHandler : IRequestHandler<UpdateRoleCommand, OperationResult<UserRole>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateRoleHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<UserRole>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<UserRole>();
        var role = await _unitOfWork.RoleRepository.GetRoleById(request.Id);
        if (role == null)
        {
            result.AddError(ErrorCode.NotFound, "Role not found");
            return result;
        }

        var roleUpdated = role.UpdateUserRole(request.Name);
        await _unitOfWork.RoleRepository.UpdateRole(roleUpdated);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        result.Payload = role;

        return result;
    }
}
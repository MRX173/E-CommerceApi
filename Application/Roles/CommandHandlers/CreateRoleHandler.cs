using Application.Enums;
using Application.Exceptions;
using Application.Models;
using Application.Roles.Commands;
using Domain.Abstractions;
using Domain.UserAggregate.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Roles.CommandHandlers;

public class CreateRoleHandler : IRequestHandler<CreateRoleCommand, OperationResult<UserRole>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateRoleHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<UserRole>> Handle(CreateRoleCommand request,
        CancellationToken cancellationToken)
    {
        var result = new OperationResult<UserRole>();
        var role = UserRole.Create(request.Name);
        await _unitOfWork.RoleRepository.CreateRole(role);
        result.Payload = role;
        return result;
    }
}
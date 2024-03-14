using Application.Models;
using Application.Roles.Commands;
using Domain.Abstractions;
using MediatR;

namespace Application.Roles.CommandHandlers;

public class DeleteRoleHandler : IRequestHandler<DeleteRoleCommand, OperationResult<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteRoleHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<bool>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<bool>();
        await _unitOfWork
            .RoleRepository
            .DeleteRole(request.Id);
        result.Payload = true;
        return result;
    }
}
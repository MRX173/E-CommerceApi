using Application.Models;
using Application.Roles.Queries;
using Domain.Abstractions;
using Domain.UserAggregate.Entities;
using MediatR;

namespace Application.Roles.QueryHandlers;

public class GetRolesHandler : IRequestHandler<GetRolesQuery, OperationResult<List<UserRole>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetRolesHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<List<UserRole>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<List<UserRole>>();
        var roles = await _unitOfWork.RoleRepository.GetRoles();
        result.Payload = roles;
        return result;
    }
}
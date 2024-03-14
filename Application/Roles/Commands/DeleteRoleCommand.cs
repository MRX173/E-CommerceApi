using Application.Models;
using MediatR;

namespace Application.Roles.Commands;

public class DeleteRoleCommand : IRequest<OperationResult<bool>>
{
    public Guid Id { get; set; }
}
using Application.Models;
using MediatR;

namespace Application.Identity.Commands;

public class RemoveUserCommand : IRequest<OperationResult<bool>>
{
    public Guid? UserId { get; set; }
}
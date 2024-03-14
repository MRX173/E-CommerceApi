using Application.Roles.Commands;
using Application.Roles.Queries;
using E_CommerceApp.Contracts.Roles.Response;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceApp.Controllers;

public class RolesController : BaseController
{
    [HttpGet]
    [Route(ApiRoutes.Role.GetById)]
    public async Task<IActionResult> GetRoleById(Guid roleId, CancellationToken cancellationToken)
    {
        var query = new GetRoleByIdQuery { Id = roleId };
        var result = await _mediator.Send(query, cancellationToken);
        var mapped = _mapper.Map<IdentityRoleResponse>(result.Payload);
        return result.IsError ? HandleErrorResponses(result.Errors) : Ok(mapped);
    }

    [HttpGet]
    [Route(ApiRoutes.Role.GetAll)]
    public async Task<IActionResult> GetRoles(CancellationToken cancellationToken)
    {
        var query = new GetRolesQuery();
        var result = await _mediator.Send(query, cancellationToken);
        var mapped = _mapper.Map<List<IdentityRoleResponse>>(result.Payload);
        return result.IsError ? HandleErrorResponses(result.Errors) : Ok(mapped);
    }
    [HttpPost]
    [Route(ApiRoutes.Role.Create)]
    public async Task<IActionResult> CreateRole([FromBody] string roleName, CancellationToken cancellationToken)
    {
        var command = new CreateRoleCommand { Name = roleName };
        var result = await _mediator.Send(command, cancellationToken);
        var mapped = _mapper.Map<IdentityRoleResponse>(result.Payload);
        return result.IsError ? HandleErrorResponses(result.Errors) : Ok(mapped);
    }

    [HttpDelete]
    [Route(ApiRoutes.Role.Delete)]
    public async Task<IActionResult> DeleteRole(Guid roleId, CancellationToken cancellationToken)
    {
        var command = new DeleteRoleCommand { Id = roleId };
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsError ? HandleErrorResponses(result.Errors) : Ok(result.Payload);
    }

    [HttpPatch]
    [Route(ApiRoutes.Role.Update)]
    public async Task<IActionResult> UpdateRole(Guid roleId, [FromBody] string roleName,
        CancellationToken cancellationToken)
    {
        var command = new UpdateRoleCommand { Id = roleId, Name = roleName };
        var result = await _mediator.Send(command, cancellationToken);
        var mapped = _mapper.Map<IdentityRoleResponse>(result.Payload);
        return result.IsError ? HandleErrorResponses(result.Errors) : Ok(mapped);
    }
}
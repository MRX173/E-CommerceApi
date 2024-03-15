using Application.Abstractions;
using Application.Identity.Commands;
using E_CommerceApp.Contracts.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceApp.Controllers;
[AllowAnonymous]
public class IdentityController : BaseController
{
    private readonly ICurrentUser _currentUser;

    public IdentityController(ICurrentUser currentUser)
    {
        _currentUser = currentUser;
    }

    [HttpPost]
    [Route(ApiRoutes.Identity.Register)]
    public async Task<IActionResult> Register([FromBody] RegisterCreate registerCreate,
        CancellationToken cancellationToken)
    {
        var command = _mapper.Map<RegisterUserCommand>(registerCreate);
        var result = await _mediator.Send(command, cancellationToken);
        var mapped = _mapper.Map<IdentityResponse>(result.Payload);
        return result.IsError ? HandleErrorResponses(result.Errors) : Ok(mapped);
    }

    [HttpPost]
    [Route(ApiRoutes.Identity.Login)]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<LoginUserCommand>(loginRequest);
        var result = await _mediator.Send(command, cancellationToken);
        var mapped = _mapper.Map<IdentityResponse>(result.Payload);
        return result.IsError ? HandleErrorResponses(result.Errors) : Ok(mapped);
    }

    [HttpPatch]
    [Route(ApiRoutes.Identity.Update)]
    public async Task<IActionResult> Update([FromBody] UpdateRequest updateRequest, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<UpdateUserCommand>(updateRequest);
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsError ? HandleErrorResponses(result.Errors) : Ok();
    }

    [HttpDelete]
    [Route(ApiRoutes.Identity.Delete)]
    public async Task<IActionResult> Delete(Guid userId, CancellationToken cancellationToken)
    {
        var command = new RemoveUserCommand { UserId = userId };
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsError ? HandleErrorResponses(result.Errors) : Ok();
    }
}
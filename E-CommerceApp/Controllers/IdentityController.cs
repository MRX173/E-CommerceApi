using Application.Abstractions;
using Application.Identity.Commands;
using E_CommerceApp.Contracts.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceApp.Controllers;

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
}
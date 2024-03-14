using Application.UserPayments.Commands;
using Application.UserPayments.Queries;
using E_CommerceApp.Contracts.Identity;
using E_CommerceApp.Contracts.UserPayments.Request;
using E_CommerceApp.Contracts.UserPayments.Response;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceApp.Controllers;

public class UserPaymentController : BaseController
{
    [HttpGet]
    [Route(ApiRoutes.UserPayment.GetByPaymentId)]
    public async Task<IActionResult> Get(Guid userPaymentId, CancellationToken cancellationToken)
    {
        var query = new GetUserPaymentByIdQuery { UserPaymentId = userPaymentId };
        var result = await _mediator.Send(query, cancellationToken);
        var mapped = _mapper.Map<UserPaymentResponse>(result.Payload);
        return result.IsError ? HandleErrorResponses(result.Errors) : Ok(mapped);
    }

    [HttpGet]
    [Route(ApiRoutes.UserPayment.GetAllByUserId)]
    public async Task<IActionResult> GetAll(Guid userId, CancellationToken cancellationToken)
    {
        var query = new GetUserPaymentsByUserIdQuery { UserId = userId };
        var result = await _mediator.Send(query, cancellationToken);
        var mapped = _mapper.Map<List<UserPaymentResponse>>(result.Payload);
        return result.IsError ? HandleErrorResponses(result.Errors) : Ok(mapped);
    }
    [HttpPost]
    [Route(ApiRoutes.UserPayment.Create)]
    public async Task<IActionResult> Create(Guid userId, [FromBody] UserPaymentCreate request,
        CancellationToken cancellationToken)
    {
        var command = _mapper.Map<CreateUserPaymentCommand>(request);
        command.UserId = userId;
        var result = await _mediator.Send(command, cancellationToken);
        var mapped = _mapper.Map<UserPaymentResponse>(result.Payload);
        return result.IsError ? HandleErrorResponses(result.Errors) : Ok(mapped);
    }
    [HttpDelete]
    [Route(ApiRoutes.UserPayment.Delete)]
    public async Task<IActionResult> Delete(Guid userPaymentId, CancellationToken cancellationToken)
    {
        var command = new DeleteUserPaymentCommand { UserPaymentId = userPaymentId };
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsError ? HandleErrorResponses(result.Errors) : Ok();
    }
}
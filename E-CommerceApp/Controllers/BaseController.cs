using Application.Enums;
using Application.Models;
using AutoMapper;
using E_CommerceApp.Contracts.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceApp.Controllers;

[Route(ApiRoutes.BaseRoute)]
[ApiController]
public class BaseController : ControllerBase
{
    private IMediator _mediatorInstance;
    private IMapper _mapperInstance;
    protected IMediator _mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>();
    protected IMapper _mapper => _mapperInstance ??= HttpContext.RequestServices.GetService<IMapper>();

    protected IActionResult HandleErrorResponses(List<Error> errors)
    {
        var apiError = new ErrorResponse();
        if (errors.Any(e => e.Code == ErrorCode.NotFound))
        {
            var error = errors.FirstOrDefault(x => x.Code == ErrorCode.NotFound);
            apiError.StatusCode = 404;
            apiError.StatusPhrase = "Not Found";
            apiError.Timestamp = DateTime.Now;
            if (error != null)
                apiError.Errors.Add(error.Message);
            return NotFound(apiError);
        }

        apiError.StatusCode = 400;
        apiError.StatusPhrase = "Bad request";
        apiError.Timestamp = DateTime.Now;
        errors.ForEach(e => apiError.Errors.Add(e.Message));
        return StatusCode(400, apiError);
    }
}
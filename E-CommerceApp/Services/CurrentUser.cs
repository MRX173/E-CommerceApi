using System.Security.Claims;
using Application.Abstractions;

namespace E_CommerceApp.Services;

public class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid? UserId => Guid.Parse(_httpContextAccessor
        .HttpContext?
        .User?
        .FindFirstValue(ClaimTypes.NameIdentifier));
}
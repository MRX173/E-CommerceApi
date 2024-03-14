using System.Security.Claims;
using Domain.UserAggregate.Entities;
using Microsoft.AspNetCore.Identity;

namespace E_CommerceApp.Services;

public static class UserServices
{
    public static string GetIdentityIdClaimValue(this HttpContext context)
    {
        return GetClaimValue("IdentityId", context);
    }

    private static string GetClaimValue(string key, HttpContext context)
    {
        var identity = context.User.Identity as ClaimsIdentity;
        return identity?.FindFirst(key)?.Value;
    }
}
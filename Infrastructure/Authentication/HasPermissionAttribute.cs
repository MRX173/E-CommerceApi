using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Authentication;

public class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(Permission permission) : base(policy:permission.ToString())
    {
    }
}
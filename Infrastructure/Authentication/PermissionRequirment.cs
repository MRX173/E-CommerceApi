using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Authentication;

public class PermissionRequirment : IAuthorizationRequirement
{
    public string Permission { get; }

    public PermissionRequirment(string permission)
    {
        Permission = permission;
    }
}
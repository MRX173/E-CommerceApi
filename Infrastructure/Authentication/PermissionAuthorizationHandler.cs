using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Authentication;

public class PermissionAuthorizationHandler :AuthorizationHandler<PermissionRequirment>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context
        , PermissionRequirment requirement)
    {
        if (context.User.HasClaim(c => c.Type == "Permission" && c.Value == requirement.Permission))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
using TTM.Core.Infrastructure.Services.Users;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace TTM.Core.Infrastructure.Auth.Permissions;

internal class PermissionAuthorizationHandler(IUserService userService) : AuthorizationHandler<PermissionRequirement>
{
    private readonly IUserService _userService = userService;

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        string? userId = context.User?.GetUserID();
        if (string.IsNullOrWhiteSpace(userId))
            ErrorHelper.ThrowUnauthorizedException();

        if (await _userService.HasPermissionAsync(new Guid(userId!), requirement.Permission))
            context.Succeed(requirement);
    }
}
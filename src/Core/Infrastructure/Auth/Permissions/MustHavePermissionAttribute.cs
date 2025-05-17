using Microsoft.AspNetCore.Authorization;
using TTM.Core.Shared.Authorization;

namespace TTM.Core.Infrastructure.Auth.Permissions;

public class MustHavePermissionAttribute : AuthorizeAttribute
{
    public MustHavePermissionAttribute(string action, string resource) =>
        Policy = APIPermission.NameFor(action, resource);
}
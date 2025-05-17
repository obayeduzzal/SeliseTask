using System.Security.Claims;

namespace TTM.Core.Infrastructure.Identity;

public interface ICurrentUserInitializer
{
    void SetCurrentUser(ClaimsPrincipal user);
    void SetCurrentUserId(string userID);
}
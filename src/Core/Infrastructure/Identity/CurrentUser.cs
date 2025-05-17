using System.Security.Claims;

namespace TTM.Core.Infrastructure.Identity;

public class CurrentUser : ICurrentUser, ICurrentUserInitializer
{
    private ClaimsPrincipal? _user;
    private Guid _userId = Guid.Empty;

    public string? Name => _user!.GetFullName();

    public Guid GetId() =>
        IsAuthenticated()
            ? Guid.Parse(_user?.GetUserID() ?? Guid.Empty.ToString())
            : _userId;

    public string? GetEmail() =>
        IsAuthenticated()
            ? _user!.GetEmail()
            : string.Empty;

    public bool IsAuthenticated() =>
        _user?.Identity?.IsAuthenticated is true;

    public IEnumerable<Claim>? GetUserClaims() =>
        _user?.Claims;

    public void SetCurrentUser(ClaimsPrincipal user)
    {
        if (_user != null)
            throw new ArgumentException("Method reserved for in-scope initialization");

        _user = user;
    }

    public void SetCurrentUserId(string userID)
    {
        if (_userId != Guid.Empty)
            throw new ArgumentException("Method reserved for in-scope initialization");

        if (!string.IsNullOrEmpty(userID))
            _userId = Guid.Parse(userID);
    }
}
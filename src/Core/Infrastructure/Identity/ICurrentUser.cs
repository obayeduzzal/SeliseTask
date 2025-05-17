using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace TTM.Core.Infrastructure.Identity;

public interface ICurrentUser
{
    string? Name { get; }
    Guid GetId();
    string? GetEmail();
    bool IsAuthenticated();
    IEnumerable<Claim>? GetUserClaims();
}
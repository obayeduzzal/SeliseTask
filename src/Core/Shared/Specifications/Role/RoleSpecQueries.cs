using System;
using System.Collections.Generic;
using Ardalis.Specification;
using TTM.Core.Domain;
using TTM.Core.Shared.Authorization;

namespace TTM.Core.Shared.Specifications;

#region Role
public class GetRoleWithClaimsByIdSpec : Specification<Role>
{
    public GetRoleWithClaimsByIdSpec(Guid id)
        => Query.Where(x => x.Id == id).Include(s => s.RoleClaims);
}

public class GetRolesByIdsSpec : Specification<Role>
{
    public GetRolesByIdsSpec(List<Guid> ids, bool includeClaims = false)
    {
        Query.Where(x => ids.Contains(x.Id));

        if (includeClaims)
            Query.Include(s => s.RoleClaims);
    }
}

public class GetRoleByNameSpec : Specification<Role>
{
    public GetRoleByNameSpec(string name)
        => Query.Where(x => x.Name.ToLower() == name.ToLower());
}

public class GetRoleByNormalizedNameSpec : Specification<Role>
{
    public GetRoleByNormalizedNameSpec(string normalizedName)
        => Query.Where(x => x.NormalizedName.ToLower() == normalizedName.ToLower());
}
#endregion

#region Role Claim
public class GetRoleClaimsByRoleIDSpec : Specification<RoleClaim>
{
    public GetRoleClaimsByRoleIDSpec(Guid roleID)
        => Query.Where(x => x.RoleId == roleID && x.ClaimType == AppClaims.Permission);
}
#endregion

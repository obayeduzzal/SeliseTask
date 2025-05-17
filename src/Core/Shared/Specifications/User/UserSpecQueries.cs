using Ardalis.Specification;
using TTM.Core.Domain;

namespace TTM.Core.Shared.Specifications;
#region User
public class GetUserByEmailSpec : Specification<User>
{
    public GetUserByEmailSpec(string email, bool includeRoles = false)
    {
        Query.Where(x => x.Email.ToLower() == email.ToLower());

        if (includeRoles)
        {
            Query.Include(s => s.UserRoles)
                 .ThenInclude(r => r.Role);
        }
    }
}

public class GetUserByIdSpec : Specification<User>
{
    public GetUserByIdSpec(Guid id, bool includeRoles = false)
    {
        Query.Where(x => x.Id == id);

        if (includeRoles)
        {
            Query.Include(s => s.UserRoles)
                 .ThenInclude(r => r.Role);
        }
    }
}
#endregion

#region UserRole
public class GetUserRoleByRoleIdSpec : Specification<UserRole>
{
    public GetUserRoleByRoleIdSpec(Guid roleId)
        => Query.Where(x => x.RoleId == roleId);
}

public class GetUserRolesByUserIdSpec : Specification<UserRole>
{
    public GetUserRolesByUserIdSpec(Guid userId, bool isTracking = false)
    {
        Query.Where(x => x.UserId == userId)
             .Include(r => r.Role)
             .ThenInclude(cl => cl!.RoleClaims);

        if (isTracking)
            Query.AsNoTracking();
    }
}
#endregion

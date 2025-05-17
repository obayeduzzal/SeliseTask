using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TTM.Core.Infrastructure.Services.Roles;

public interface IRoleService : ITransientDependency
{
    #region Role
    Task<bool> IsExistAsync(string name, CancellationToken ct = default);
    Task<bool> HasUsersInRoleAsync(Guid roleID, CancellationToken ct = default);
    Task<List<Role>> GetRolesByIdsAsync(List<Guid> ids, CancellationToken ct = default);
    RoleDetailsDTO GetRoleDetailsDTO(Role role);
    #endregion

    #region Role Claim
    List<RoleClaim> GetRoleClaimsByPermissions(List<string> permissions);
    #endregion
}
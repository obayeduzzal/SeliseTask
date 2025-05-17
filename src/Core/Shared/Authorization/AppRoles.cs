using TTM.Core.Shared.DTOs;
using System.Collections.ObjectModel;

namespace TTM.Core.Shared.Authorization;

public static class AppRoles
{
    public const string ADMIN = nameof(ADMIN);
    public const string MANAGER = nameof(MANAGER);
    public const string EMPLOYEE = nameof(EMPLOYEE);

    public static IReadOnlyList<ApplicationSystemRoleDTO> ApplicationSystemRoles { get; } = new ReadOnlyCollection<ApplicationSystemRoleDTO>(
    [
        new ApplicationSystemRoleDTO
        {
            Name = "Admin",
            NormalizedName = ADMIN
        },
        new ApplicationSystemRoleDTO
        {
            Name = "Manager",
            NormalizedName = MANAGER
        },
        new ApplicationSystemRoleDTO
        {
            Name = "Employee",
            NormalizedName = EMPLOYEE
        }
    ]);
}
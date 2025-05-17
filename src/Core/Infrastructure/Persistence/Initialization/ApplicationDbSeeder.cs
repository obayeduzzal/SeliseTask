using TTM.Core.Shared.Authorization;

namespace TTM.Core.Infrastructure.Persistence.Initialization;

internal class ApplicationDbSeeder(
    IRepository<Role> roleRepository,
    IRepository<User> userRepository,
    IRepository<RoleClaim> roleClaimRepository,
    ILogger<ApplicationDbSeeder> logger)
{
    private readonly IRepository<Role> _roleRepository = roleRepository;
    private readonly IRepository<User> _userRepository = userRepository;
    private readonly IRepository<RoleClaim> _roleClaimRepository = roleClaimRepository;
    private readonly ILogger<ApplicationDbSeeder> _logger = logger;

    public async Task SeedDataAsync(CancellationToken ct)
    {
        await SeedRolesAsync(ct);

        await SeedSuperAdminUserAsync(ct);

        await SeedManangerUserAsync(ct);

        await SeedEmployeeUserAsync(ct);
    }

    private async Task SeedRolesAsync(CancellationToken ct)
    {
        foreach (var systemRole in AppRoles.ApplicationSystemRoles.ToList())
        {
            var role = await _roleRepository.FirstOrDefaultAsync(new GetRoleByNormalizedNameSpec(systemRole.NormalizedName.Trim()), ct);
            if (role == null)
            {
                _logger.LogInformation("Seeding {Role} Role.", systemRole.Name);

                role = Role.Create(systemRole.Name, systemRole.NormalizedName.ToUpper(), true);

                await _roleRepository.AddAsync(role, ct);
            }

            if (systemRole.NormalizedName == AppRoles.ADMIN)
                await AssignPermissionsToRoleAsync(role, AppPermissions.Admin, ct);

            if (systemRole.NormalizedName == AppRoles.MANAGER)
                await AssignPermissionsToRoleAsync(role, AppPermissions.Manager, ct);

            if (systemRole.NormalizedName == AppRoles.EMPLOYEE)
                await AssignPermissionsToRoleAsync(role, AppPermissions.Employee, ct);

        }
    }

    private async Task AssignPermissionsToRoleAsync(
        Role role,
        IReadOnlyList<APIPermission> permissions,
        CancellationToken ct)
    {
        var currentClaims = (await _roleClaimRepository.ListAsync(new GetRoleClaimsByRoleIDSpec(role.Id), cancellationToken: ct)).ToList();

        var permissionNames = permissions
            .Where(permission => !currentClaims.Any(c => c.ClaimName.Equals(permission.Name)))
            .Select(permission => permission.Name)
            .ToList();

        var roleClaims = new List<RoleClaim>();
        permissionNames.ForEach(permissionName =>
        {
            _logger.LogInformation("Seeding {Role} Permission '{Permission}'.", role.Name, permissionName);

            roleClaims.Add(new RoleClaim
            {
                RoleId = role.Id,
                ClaimType = AppClaims.Permission,
                ClaimName = permissionName,
            });
        });

        if (roleClaims.Count > 0)
            await _roleClaimRepository.AddRangeAsync(roleClaims, ct);
    }

    private async Task SeedSuperAdminUserAsync(CancellationToken ct)
    {
        string adminEmail = "admin@demo.com";
        var user = await _userRepository.FirstOrDefaultAsync(new GetUserByEmailSpec(adminEmail), ct);
        if (user is null)
        {
            var role = await _roleRepository.FirstOrDefaultAsync(new GetRoleByNormalizedNameSpec(AppRoles.ADMIN), ct);

            user = User.CreateUser(
                fullName: "Admin Demo",
                email: adminEmail,
                passWord: "Admin123!".HashPassword(),
                isActive: true,
                roles: role != null
                    ? [UserRole.Create(role)]
                    : new());

            _logger.LogInformation("Seeding Admin user: {FullName}", user.FullName);

            await _userRepository.AddAsync(user, ct);
        }
    }

    private async Task SeedManangerUserAsync(CancellationToken ct)
    {
        string managerEmail = "manager@demo.com";
        var user = await _userRepository.FirstOrDefaultAsync(new GetUserByEmailSpec(managerEmail), ct);
        if (user is null)
        {
            var role = await _roleRepository.FirstOrDefaultAsync(new GetRoleByNormalizedNameSpec(AppRoles.ADMIN), ct);

            user = User.CreateUser(
                fullName: "Manager Demo",
                email: managerEmail,
                passWord: "Manager123!".HashPassword(),
                isActive: true,
                roles: role != null
                    ? [UserRole.Create(role)]
                    : new());

            _logger.LogInformation("Seeding Manager user: {FullName}", user.FullName);

            await _userRepository.AddAsync(user, ct);
        }
    }

    private async Task SeedEmployeeUserAsync(CancellationToken ct)
    {
        string employeeEmail = "employee@demo.com";
        var user = await _userRepository.FirstOrDefaultAsync(new GetUserByEmailSpec(employeeEmail), ct);
        if (user is null)
        {
            var role = await _roleRepository.FirstOrDefaultAsync(new GetRoleByNormalizedNameSpec(AppRoles.ADMIN), ct);

            user = User.CreateUser(
                fullName: "Manager Demo",
                email: employeeEmail,
                passWord: "Manager123!".HashPassword(),
                isActive: true,
                roles: role != null
                    ? [UserRole.Create(role)]
                    : new());

            _logger.LogInformation("Seeding Employee user: {FullName}", user.FullName);

            await _userRepository.AddAsync(user, ct);
        }
    }
}
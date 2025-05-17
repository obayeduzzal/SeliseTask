using System.Collections.ObjectModel;
using System.Linq;
using TTM.Core.Domain;

namespace TTM.Core.Shared.Authorization;
public static class AppPermissions
{
    private static readonly APIPermission[] _permissions =
    [
        new("Search Users", "Users", AppActions.Search, AppResources.Users, RoleType.Admin),
        new("View User", "Users", AppActions.View, AppResources.Users, RoleType.Admin),
        new("Create User", "Users", AppActions.Create, AppResources.Users, RoleType.Admin),
        new("Update User", "Users", AppActions.Update, AppResources.Users, RoleType.Admin),
        new("Delete User", "Users", AppActions.Delete, AppResources.Users, RoleType.Admin),

        new("Search Teams", "Teams", AppActions.Search, AppResources.Teams, RoleType.Admin),
        new("View Teams", "Teams", AppActions.View, AppResources.Teams, RoleType.Admin),
        new("Create Teams", "Teams", AppActions.Create, AppResources.Teams, RoleType.Admin),
        new("Update Teams", "Teams", AppActions.Update, AppResources.Teams, RoleType.Admin),
        new("Delete Teams", "Teams", AppActions.Delete, AppResources.Teams, RoleType.Admin),

        new("Search Task", "Task", AppActions.Search, AppResources.Tasks, RoleType.Manager),
        new("View Task", "Task", AppActions.View, AppResources.Tasks, RoleType.Manager),
        new("Create Task", "Task", AppActions.Create, AppResources.Tasks, RoleType.Manager),
        new("Update Task", "Task", AppActions.Update, AppResources.Tasks, RoleType.Manager),
        new("Delete Task", "Task", AppActions.Delete, AppResources.Tasks, RoleType.Manager),

        new("View Task", "Task", AppActions.View, AppResources.Tasks, RoleType.Employee),
        new("Update Task", "Task", AppActions.Update, AppResources.Tasks, RoleType.Employee),
    ];

    public static IReadOnlyList<APIPermission> All { get; }
        = new ReadOnlyCollection<APIPermission>(_permissions);

    public static IReadOnlyList<APIPermission> Admin { get; }
        = new ReadOnlyCollection<APIPermission>(_permissions);

    public static IReadOnlyList<APIPermission> Manager { get; }
        = new ReadOnlyCollection<APIPermission>([.._permissions.Where(i => i.RoleType == RoleType.Manager)]);

    public static IReadOnlyList<APIPermission> Employee { get; }
        = new ReadOnlyCollection<APIPermission>([.. _permissions.Where(i => i.RoleType == RoleType.Employee)]);
}

public record APIPermission(
    string Description,
    string DisplayName,
    string Action,
    string Resource,
    RoleType RoleType)
{
    public string Name => NameFor(Action, Resource);
    public static string NameFor(string action, string resource) => $"{AppClaims.Permission}.{resource}.{action}";
}
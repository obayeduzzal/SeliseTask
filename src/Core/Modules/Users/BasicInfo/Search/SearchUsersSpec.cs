namespace TTM.Core.Modules.Users.BasicInfo.Search;

public class SearchUsersSpec : EntitiesByPaginationFilterSpec<User, User>
{
    public SearchUsersSpec(SearchUsersCommand command)
        : base(command)
    {
        Query
            .OrderBy(u => u.FullName)
            .Where(
                u => u.FullName.Contains(command.Name!.ToLower()),
                !string.IsNullOrEmpty(command.Name))
            .Where(
                u => u.Email.Contains(command.Email!.ToLower()),
                !string.IsNullOrEmpty(command.Email))
            .Where(
                u => u.IsActive == command.IsActive,
                command.IsActive.HasValue);
    }
}
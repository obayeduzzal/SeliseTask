namespace TTM.Core.Modules.Users.BasicInfo.Search;

public class SearchUsersCommand : PaginationFilter, IRequest<PagedData<UserSearchDTO>>
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public bool? IsActive { get; set; }
}

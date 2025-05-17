namespace TTM.Core.Modules.Users.BasicInfo.Search;

public class SearchUsersHandler(IRepository<User> userRepository) : IRequestHandler<SearchUsersCommand, PagedData<UserSearchDTO>>
{
    private readonly IRepository<User> _userRepository = userRepository;

    public async Task<PagedData<UserSearchDTO>> Handle(SearchUsersCommand command, CancellationToken ct)
    {
        var spec = new SearchUsersSpec(command);

        var users = await _userRepository.ListAsync(spec, ct);
        int totalCount = await _userRepository.CountAsync(spec, ct);

        return new PagedData<UserSearchDTO>
        {
            Data = users.Adapt<List<UserSearchDTO>>(),
            Meta = new Paging(totalCount, command.CurrentPage, command.PageSize).Meta()
        };
    }
}

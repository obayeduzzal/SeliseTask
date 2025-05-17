namespace TTM.Core.Modules.Users.BasicInfo.Delete;

#region Handler
public sealed class DeleteUserHandler(
    IRepository<User> userRepository) : IRequestHandler<DeleteUserCommand, ResponseMetaDTO>
{
    private readonly IRepository<User> _userRepository = userRepository;

    public async Task<ResponseMetaDTO> Handle(
      DeleteUserCommand command,
      CancellationToken ct)
    {
        var user = await _userRepository.FirstOrDefaultAsync(new GetUserByIdSpec(command.Id, includeRoles: true), ct);
        if (user == null)
            ErrorHelper.ThrowNotFoundException("User", "User Not Found!");

        await _userRepository.DeleteAsync(user!, ct);

        return new ResponseMetaDTO
        {
            Message = "User has been deleted successfully."
        };
    }
}
#endregion

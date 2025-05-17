namespace TTM.Core.Modules.Users.BasicInfo.Update;

public class UpdateUserCommand : IRequest<UserDTO>
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = default!;
    public List<Guid> RoleIds { get; set; } = [];
}
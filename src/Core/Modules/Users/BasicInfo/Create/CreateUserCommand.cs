namespace TTM.Core.Modules.Users.BasicInfo.Create;

public class CreateUserCommand : IRequest<ResponseMetaDTO>
{
    /// <example>user@example.com.</example>
    public string Email { get; set; } = default!;
    public string FullName { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string ConfirmPassword { get; set; } = default!;
    public List<Guid> RoleIds { get; set; } = [];
}
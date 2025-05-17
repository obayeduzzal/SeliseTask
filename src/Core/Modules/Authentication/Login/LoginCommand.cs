namespace TTM.Core.Modules.Authentication.Login;
#region Request
public class LoginCommand : IRequest<AuthDTO>
{
    /// <example>user@example.com</example>
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}
#endregion

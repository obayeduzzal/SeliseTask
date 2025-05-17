namespace TTM.Core.Shared.DTOs;
public class AuthDTO
{
    public AccessTokenDTO? TokenInfo { get; set; }
    public UserDTO? UserInfo { get; set; }
}

#region Token DTOs
public class AccessTokenDTO
{
    public string AccessToken { get; set; } = default!;
    public DateTime AccessTokenExpiryTime { get; set; }
}
#endregion
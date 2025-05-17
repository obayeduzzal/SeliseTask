using Microsoft.AspNetCore.SignalR;

namespace TTM.Core.Infrastructure.SignalR;
public class UserIDProvider : IUserIdProvider
{
    public string GetUserId(HubConnectionContext connection)
    {
        string? userID = connection.User.Claims?.FirstOrDefault(s => s.Type == "UserID")?.Value;

        return userID ?? string.Empty;
    }
}
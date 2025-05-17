namespace TTM.Core.Modules.Users.BasicInfo.Get;

public class GetUserRequest(Guid id) : IRequest<UserDTO>
{
    public Guid Id { get; set; } = id;
}

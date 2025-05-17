namespace TTM.Core.Modules.Users.BasicInfo.Delete;

public class DeleteUserCommand(Guid id) : IRequest<ResponseMetaDTO>
{
    public Guid Id { get; set; } = id;
}
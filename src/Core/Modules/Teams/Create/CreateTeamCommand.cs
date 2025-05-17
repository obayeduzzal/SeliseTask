namespace TTM.Core.Modules.Teams.Create;
public class CreateTeamCommand : IRequest<ResponseMetaDTO>
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public List<Guid> TeamMembersId { get; set; }
}

namespace TTM.Core.Modules.Teams.Get;
public class GetTeamQuery(Guid teamId) : IRequest<TeamDTO>
{
    public Guid TeamId { get; set; } = teamId;
}

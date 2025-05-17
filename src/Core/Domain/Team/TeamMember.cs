namespace TTM.Core.Domain.Team;
public class TeamMember : IAggregateRoot
{
    public Guid TeamId { get; private set; }
    public Guid UserId { get; private set; }

    public static TeamMember Create(Guid teamId, Guid usrId)
    {
        return new TeamMember
        {
            TeamId = teamId,
            UserId = usrId
        };
    }
}

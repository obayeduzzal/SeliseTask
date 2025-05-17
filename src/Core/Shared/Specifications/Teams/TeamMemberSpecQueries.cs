using Ardalis.Specification;
using TTM.Core.Domain.Team;

namespace TTM.Core.Shared.Specifications.Teams;
public class GetTeamMembersById: Specification<TeamMember>
{
    public GetTeamMembersById(Guid teamId)
    {
        Query.Where(i => i.TeamId.Equals(teamId));
    }
}

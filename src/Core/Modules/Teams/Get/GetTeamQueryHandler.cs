using TTM.Core.Domain.Team;
using TTM.Core.Shared.Specifications.Teams;

namespace TTM.Core.Modules.Teams.Get;
public class GetTeamQueryHandler(IReadRepository<Team> teamReadRepository, IReadRepository<TeamMember> teamMemberReadRepository) : IRequestHandler<GetTeamQuery, TeamDTO>
{
    private readonly IReadRepository<Team> _teamReadRepository = teamReadRepository;
    private readonly IReadRepository<TeamMember> _teamMemberReadRepository = teamMemberReadRepository;
    public async Task<TeamDTO> Handle(GetTeamQuery request, CancellationToken cancellationToken)
    {
        var team = await _teamReadRepository.GetByIdAsync(request.TeamId, cancellationToken);
        if (team == null)
            ErrorHelper.ThrowNotFoundException("Team", "Team not found");

        var teamMembers = await _teamMemberReadRepository.ListAsync(new GetTeamMembersById(request.TeamId), cancellationToken);

        var teamDto = team.Adapt<TeamDTO>();

        return teamDto;
    }
}
